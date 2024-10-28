using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;

        public DetailsModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var result = await _appointmentService.GetByIdAsync(id);

            if (!result.IsSuccess || result.Object == null)
            {
                return RedirectToPage("Index");
            }

            Appointment = result.Object as Appointment;
            return Page();
        }
    }
}
