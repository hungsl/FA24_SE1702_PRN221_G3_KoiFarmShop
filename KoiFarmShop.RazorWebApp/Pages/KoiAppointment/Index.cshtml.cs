using System.Security.Claims;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IAppointmentService appointmentService, ILogger<IndexModel> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Get the Customer ID of the logged-in user
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                _logger.LogWarning("User ID not found or invalid. User may not be logged in.");
                ErrorMessage = "Could not retrieve user ID. Please log in.";
                return Page();
            }

            // Retrieve appointments for this user
            var result = await _appointmentService.GetAppointmentsByUserIdAsync(userId);
            if (result.IsSuccess && result.Object is List<Appointment> appointments)
            {
                Appointments = appointments;
            }
            else
            {
                _logger.LogWarning("No appointments found for user ID: {UserId}", userId);
                ErrorMessage = result.Error?.Description ?? "No appointments found.";
            }

            return Page();
        }
    }
}
