using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Infrastructure.Implement.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(KVSCContext context) : base(context) { }

        // CREATE
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> GetByIdAsync(Guid appointmentId)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Customer)
                .Include(a => a.Pet)
                .Include(a => a.PetService)
                .Include(a => a.ComboService)
                .Include(a => a.AppointmentVeterinarians)
                    .ThenInclude(av => av.Veterinarian)
                .FirstOrDefaultAsync(a => a.Id == appointmentId && !a.IsDeleted);

            // Kiểm tra nếu appointment là null, trả về null hoặc xử lý tùy ý
            if (appointment == null)
            {
                return null; // Hoặc có thể trả về một giá trị mặc định khác
            }

            // Nếu muốn bảo vệ các thuộc tính điều hướng bên trong appointment
            appointment.Customer = appointment.Customer ?? new User(); // Đảm bảo không null
            appointment.Pet = appointment.Pet ?? new Pet();
            appointment.PetService = appointment.PetService ?? new PetService();
            appointment.ComboService = appointment.ComboService ?? new ComboService();
            appointment.AppointmentVeterinarians = appointment.AppointmentVeterinarians ?? new List<AppointmentVeterinarian>();

            return appointment;
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
