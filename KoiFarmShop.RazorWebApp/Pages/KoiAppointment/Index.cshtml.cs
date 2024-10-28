using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public IndexModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            var result = await _appointmentService.GetAllAppointmentsAsync();

            if (result.IsSuccess && result.Object is List<Appointment> appointments)
            {
                Appointments = appointments;
            }
            else
            {
                ErrorMessage = result.Error?.Description ?? "An unexpected error occurred.";
            }
        }
    }
}
