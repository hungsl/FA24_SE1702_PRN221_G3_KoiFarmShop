using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class DetailsModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IAppointmentService appointmentService, ILogger<DetailsModel> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            // Retrieve appointment by ID
            var result = await _appointmentService.GetByIdAsync(id);

            // Check if the result was successful and if the appointment object is not null
            if (!result.IsSuccess || result.Object == null)
            {
                _logger.LogWarning("Appointment with ID {Id} was not found or could not be retrieved.", id);
                TempData["ErrorMessage"] = "The requested appointment could not be found.";
                return RedirectToPage("Index");
            }

            Appointment = result.Object as Appointment;

            // Check if casting was successful
            if (Appointment == null)
            {
                _logger.LogError("Failed to cast the appointment result object to Appointment for ID {Id}.", id);
                TempData["ErrorMessage"] = "An error occurred while loading the appointment details.";
                return RedirectToPage("Index");
            }

            return Page();
        }
    }

}
