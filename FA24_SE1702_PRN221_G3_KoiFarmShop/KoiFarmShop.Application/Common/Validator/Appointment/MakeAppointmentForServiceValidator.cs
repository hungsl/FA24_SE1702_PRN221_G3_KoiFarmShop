using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.Interface;

namespace KoiFarmShop.Application.Common.Validator.Appointment
{
    public class MakeAppointmentForServiceValidator : AppointmentForServiceValidator<MakeAppointmentForServiceRequest>
    {
        public MakeAppointmentForServiceValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            MakeAppointmentForServiceRules(request => request.CustomerId, "Customer");
            MakeAppointmentForServiceRules(request => request.PetId, "Pet");
        }
    }

}
