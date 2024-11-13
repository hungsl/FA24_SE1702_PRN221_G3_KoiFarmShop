using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class SignalRAppointmentServiceModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public SignalRAppointmentServiceModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public List<Appointment> AppointmentServices { get; set; } = new(); // Ensure Appointment is the correct model

        public async Task OnGetAsync() // Use async Task instead of void
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();

            if (result.IsSuccess)
            {
                AppointmentServices = result.Object as List<Appointment>; // Ensure the type contains Id
            }
            else
            {
                AppointmentServices = new List<Appointment>();
            }
        }
    }

}
