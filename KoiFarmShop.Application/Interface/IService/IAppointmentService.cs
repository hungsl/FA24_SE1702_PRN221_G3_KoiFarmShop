using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IAppointmentService
    {
        public Task<Result> MakeAppointmentForServiceAsync(MakeAppointmentForServiceRequest request);
        public Task<Result> MakeAppointmentForComboAsync(MakeAppointmentForComboRequest request);
    }
}
