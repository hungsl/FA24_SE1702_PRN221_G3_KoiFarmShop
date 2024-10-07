using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Appointment
{
    public class MakeAppointmentForServiceValidator : AppointmentForServiceValidator<MakeAppointmentForServiceRequest>
    {
        public MakeAppointmentForServiceValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            MakeAppointmentForServiceRules(request => request.CustomerId, "Customer");
            MakeAppointmentForServiceRules(request => request.CustomerId, "Pet");
        }
    }
}
