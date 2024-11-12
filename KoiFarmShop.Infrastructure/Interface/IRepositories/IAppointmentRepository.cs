using KoiFarmShop.Domain.Entities;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {

        Task<IEnumerable<Appointment>> SearchAppointmentsAsync(string customerName = null, string petName = null, DateTime? appointmentDate = null);
        public Task<Appointment> CreateAppointmentAsync(Appointment appointment);

        // READ (các phương thức khác nếu cần)
        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(Guid userId);

        public Task<Veterinarian> GetAvailableVeterinarianAsync(DateTime appointmentDate);
        public Task UpdateScheduleAvailabilityAsync(Guid veterinarianId, DateTime appointmentDate);

        //UPDATE
        Task<Appointment> UpdateAppointmentAsync(Guid appointmentId, Appointment updatedAppointment);

        //DELETE
        Task<bool> DeleteAppointmentAsync(Guid appointmentId);
    }
}
