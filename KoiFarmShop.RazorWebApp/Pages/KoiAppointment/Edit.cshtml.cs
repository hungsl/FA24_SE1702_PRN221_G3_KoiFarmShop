using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class EditModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IVeterinarianService _veterinarianService;
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceLogic _petServiceLogic;
        private readonly ILogger<EditModel> _logger;

        public EditModel(
            IAppointmentService appointmentService,
            IPetServiceService petServiceService,
            IPetServiceLogic petServiceLogic,
            ILogger<EditModel> logger,
            IVeterinarianService veterinarianService)
        {
            _appointmentService = appointmentService;
            _petServiceService = petServiceService;
            _petServiceLogic = petServiceLogic;
            _logger = logger;
            _veterinarianService = veterinarianService;
        }

        [BindProperty]
        public Guid AppointmentId { get; set; }


        [BindProperty]
        [Required]
        public Guid CustomerId { get; set; }

        [BindProperty]
        [Required]
        public Guid PetId { get; set; }

        [BindProperty]
        [Required]
        public Guid PetServiceId { get; set; }

        [BindProperty]
        public List<Guid> VeterinarianIds { get; set; } = new List<Guid>();

        [BindProperty]
        [Required]
        public DateTime AppointmentDate { get; set; }

        public List<PetService> PetServices { get; set; } = new List<PetService>();
        public List<Pet> Pets { get; set; } = new List<Pet>();
        public List<Veterinarian> Veterinarians { get; set; } = new List<Veterinarian>();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            AppointmentId = id;
            _logger.LogInformation("Set AppointmentId to: {AppointmentId}", AppointmentId);

            // Retrieve the logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var customerId))
            {
                _logger.LogWarning("Could not retrieve or parse the user ID.");
                ModelState.AddModelError(string.Empty, "Could not retrieve or parse the user ID.");
                return Page();
            }

            CustomerId = customerId;
            _logger.LogInformation("Set CustomerId to: {CustomerId}", CustomerId);

            // Fetch pet services
            var serviceResult = await _petServiceService.GetAllPetServicesAsync();
            if (serviceResult.IsSuccess && serviceResult.Object is List<PetService> petServices)
            {
                PetServices = petServices;
                _logger.LogInformation("Fetched {Count} pet services.", petServices.Count);
            }
            else
            {
                _logger.LogError("Failed to load pet services.");
                ModelState.AddModelError(string.Empty, "Failed to load pet services.");
            }

            // Fetch pets for this customer
            var petResult = await _petServiceLogic.GetPetsByOwnerIdAsync(CustomerId);
            if (petResult.IsSuccess && petResult.Object is List<Pet> pets)
            {
                Pets = pets;
                _logger.LogInformation("Fetched {Count} pets for CustomerId: {CustomerId}", pets.Count, CustomerId);
            }
            else
            {
                _logger.LogWarning("No pets found for customer ID: {CustomerId}", CustomerId);
                ModelState.AddModelError(string.Empty, "No pets found for this customer.");
            }

            // Fetch veterinarians
            var veterinarianResult = await _veterinarianService.GetAllVeterinariansAsync();
            if (veterinarianResult.IsSuccess && veterinarianResult.Object is List<Veterinarian> veterinarians)
            {
                Veterinarians = veterinarians;
                _logger.LogInformation("Fetched {Count} veterinarians.", veterinarians.Count);
            }
            else
            {
                _logger.LogError("Failed to load veterinarians.");
                ModelState.AddModelError(string.Empty, "Failed to load veterinarians.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Entered OnPostAsync with AppointmentId: {AppointmentId}", AppointmentId);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return Page();
            }

            // Log details of the update request for troubleshooting
            _logger.LogInformation("Creating update request with CustomerId: {CustomerId}, PetId: {PetId}, PetServiceId: {PetServiceId}, AppointmentDate: {AppointmentDate}",
                                   CustomerId, PetId, PetServiceId, AppointmentDate);

            // Create a MakeAppointmentForServiceRequest from the bound properties
            var updateRequest = new MakeAppointmentForServiceRequest
            {
                CustomerId = CustomerId,
                PetId = PetId,
                PetServiceId = PetServiceId,
                VeterinarianIds = VeterinarianIds,
                AppointmentDate = AppointmentDate
            };

            // Call the update method in the service layer
            var result = await _appointmentService.UpdateAppointmentAsync(AppointmentId, updateRequest);

            if (result.IsFailure)
            {
                _logger.LogWarning("Failed to update appointment with ID: {AppointmentId}", AppointmentId);

                // Log each error and add them to the ModelState to display on the page
                foreach (var error in result.Errors)
                {
                    _logger.LogError("Error updating appointment: {Error}", error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            _logger.LogInformation("Successfully updated appointment with ID: {AppointmentId}", AppointmentId);
            return RedirectToPage("/KoiAppointment/Index"); // Adjust the redirect as needed
        }



    }
}
