using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> SearchAppointmentsAsync(string customerName = null, string petName = null, DateTime? appointmentDate = null);
        Task<Result> UpdateAppointmentAsync(Guid appointmentId, MakeAppointmentForServiceRequest request);
        public Task<Result> MakeAppointmentForServiceAsync(MakeAppointmentForServiceRequest request);
        public Task<Result> MakeAppointmentForComboAsync(MakeAppointmentForComboRequest request);
        public Task<Result> GetAllAppointmentsAsync();
        public Task<Result> GetByIdAsync(Guid appointmentId);
        Task<Result> GetAppointmentsByUserIdAsync(Guid userId);

        Task<Result> DeleteAppointmentAsync(Guid appointmentId);

    }
}
