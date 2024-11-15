using KoiFarmShop.Domain.Entities;
<<<<<<< HEAD
=======
using KoiFarmShop.Infrastructure.Implement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> Dev_Danh_skibidi

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
<<<<<<< HEAD

        Task<IEnumerable<Appointment>> SearchAppointmentsAsync(string customerName = null, string petName = null, DateTime? appointmentDate = null);
=======
>>>>>>> Dev_Danh_skibidi
        public Task<Appointment> CreateAppointmentAsync(Appointment appointment);

        // READ (các phương thức khác nếu cần)
        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
<<<<<<< HEAD
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId);

        public Task<Veterinarian> GetAvailableVeterinarianAsync(DateTime appointmentDate);
        public Task UpdateScheduleAvailabilityAsync(Guid veterinarianId, DateTime appointmentDate);

        //UPDATE
        Task<Appointment> UpdateAppointmentAsync(Guid appointmentId, Appointment updatedAppointment);

        //DELETE
        Task<bool> DeleteAppointmentAsync(Guid appointmentId);
=======

        public Task<Veterinarian> GetAvailableVeterinarianAsync(DateTime appointmentDate);
        public Task UpdateScheduleAvailabilityAsync(Guid veterinarianId, DateTime appointmentDate);
>>>>>>> Dev_Danh_skibidi
    }
}
