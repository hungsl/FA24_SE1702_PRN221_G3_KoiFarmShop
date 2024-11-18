using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Implement.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(KVSCContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> SearchAppointmentsAsync(string customerName = null, string petName = null, DateTime? appointmentDate = null)
        {
            var query = _context.Appointments
                .Where(a => !a.IsDeleted)
                .Include(a => a.Customer)       // Eager loading Customer details
                .Include(a => a.Pet)            // Eager loading Pet details
                .Include(a => a.PetService)     // Eager loading PetService details
                .Include(a => a.ComboService)   // Eager loading ComboService details
                .AsQueryable();

            // Apply filters based on provided parameters

            // Customer Name
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                query = query.Where(a => a.Customer.FullName.Contains(customerName));
            }

            // Pet Name
            if (!string.IsNullOrWhiteSpace(petName))
            {
                query = query.Where(a => a.Pet.Name.Contains(petName));
            }

            // Appointment Date
            if (appointmentDate.HasValue)
            {
                query = query.Where(a => a.AppointmentDate.Date == appointmentDate.Value.Date); // Comparing only the date part
            }

            return await query.ToListAsync();
        }


        // CREATE
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }



        //UPDATE
        public async Task<Appointment> UpdateAppointmentAsync(Guid appointmentId, Appointment updatedAppointment)
        {
            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(a => a.Id == appointmentId && !a.IsDeleted);

            if (appointment == null)
            {
                throw new KeyNotFoundException($"Appointment with ID {appointmentId} was not found.");
            }

            // Update fields
            appointment.CustomerId = updatedAppointment.CustomerId;
            appointment.PetId = updatedAppointment.PetId;
            appointment.PetServiceId = updatedAppointment.PetServiceId;
            appointment.AppointmentDate = updatedAppointment.AppointmentDate;
            appointment.Status = updatedAppointment.Status;

            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task UpdateScheduleAvailabilityAsync(Guid veterinarianId, DateTime appointmentDate)
        {
            var appointmentDay = appointmentDate.Date;
            var appointmentTime = appointmentDate.TimeOfDay;

            var schedule = await _context.VeterinarianSchedules
                .FirstOrDefaultAsync(s => s.VeterinarianId == veterinarianId
                    && s.Date == appointmentDate.Date
                    && s.StartTime <= appointmentTime
                    && s.EndTime >= appointmentTime);

            if (schedule != null)
            {
                schedule.IsAvailable = false;
                _context.VeterinarianSchedules.Update(schedule);
                await _context.SaveChangesAsync();
            }
        }



        // READ (các phương thức khác nếu cần)
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Where(a => !a.IsDeleted)
                .Include(a => a.Customer)       // Eager loading Customer details
                .Include(a => a.Pet)            // Eager loading Pet details
                .Include(a => a.PetService)     // Eager loading PetService details
                .Include(a => a.ComboService)   // Eager loading ComboService details
                .ToListAsync();
        }



       
        public async Task<Veterinarian> GetAvailableVeterinarianAsync(DateTime appointmentDate)
        {
            var appointmentDay = appointmentDate.Date;
            var appointmentTime = appointmentDate.TimeOfDay;

            var availableVeterinarian = await _context.Veterinarians
            .Include(v => v.VeterinarianSchedules)
            .Where(v => v.VeterinarianSchedules.Any(s =>
                s.Date == appointmentDay &&
                s.StartTime <= appointmentTime &&
                s.EndTime >= appointmentTime &&
                s.IsAvailable
            ))
            .FirstOrDefaultAsync();


            return availableVeterinarian;
        }
        public async Task<Appointment?> GetByIdAsync(Guid appointmentId)
        {
            return await _context.Appointments
                .Include(a => a.Customer)     // Include Customer
                .Include(a => a.Pet)          // Include Pet
                .Include(a => a.PetService)   // Include Pet Service
                .Include(a => a.ComboService) // Include Combo Service (if applicable)
                .FirstOrDefaultAsync(a => a.Id == appointmentId && !a.IsDeleted);
        }



        public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId)
        {
            return await _context.Appointments
                .Where(a => a.CustomerId == userId && !a.IsDeleted) // Filter by CustomerId and exclude deleted appointments
                .Include(a => a.Customer)
                .Include(a => a.Pet)
                .Include(a => a.PetService)
                .Include(a => a.ComboService)
                .ToListAsync();
        }





        //DELETE
        // Soft delete an appointment
        public async Task<bool> DeleteAppointmentAsync(Guid appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);

            if (appointment == null || appointment.IsDeleted)
            {
                return false; // Appointment not found or already deleted
            }

            appointment.IsDeleted = true;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
