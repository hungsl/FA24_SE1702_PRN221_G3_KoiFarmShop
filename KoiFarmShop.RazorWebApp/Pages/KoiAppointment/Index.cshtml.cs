using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

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
        [BindProperty(SupportsGet = true)]
        public string? CustomerName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PetName { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? AppointmentDate { get; set; }
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
            if (!result.IsSuccess || result.Object == null)
            {
                _logger.LogWarning("Failed to retrieve appointments for user ID: {UserId}. Error: {Error}", userId, result.Error?.Description);
                ErrorMessage = result.Error?.Description ?? "No appointments found.";
                return Page();
            }

            var appointments = result.Object as List<Appointment> ?? new List<Appointment>();

            // Apply search filters
            if (!string.IsNullOrEmpty(CustomerName))
            {
                appointments = appointments
                    .Where(a => a.Customer.FullName.Contains(CustomerName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(PetName))
            {
                appointments = appointments
                    .Where(a => a.Pet.Name.Contains(PetName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (AppointmentDate.HasValue)
            {
                appointments = appointments
                    .Where(a => a.AppointmentDate.Date == AppointmentDate.Value.Date)
                    .ToList();
            }

            // Assign the filtered list to the Appointments property
            Appointments = appointments;

            return Page();
        }

    }
}
