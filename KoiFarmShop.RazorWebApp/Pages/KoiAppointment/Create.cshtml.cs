using System.Collections;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
   public class CreateModel : PageModel
{
    private readonly IAppointmentService _appointmentService;
    private readonly IVeterinarianService _veterinarianService;
    private readonly IPetServiceService _petServiceService;
    private readonly IPetServiceLogic _petServiceLogic;
    private readonly ILogger<CreateModel> _logger;

    public CreateModel(
        IAppointmentService appointmentService,
        IPetServiceService petServiceService,
        IPetServiceLogic petServiceLogic,
        ILogger<CreateModel> logger,
        IVeterinarianService veterinarianService)
    {
        _appointmentService = appointmentService;
        _petServiceService = petServiceService;
        _petServiceLogic = petServiceLogic;
        _logger = logger;
        _veterinarianService = veterinarianService;
    }

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

    public async Task<IActionResult> OnGetAsync()
    {
        // Retrieve the logged-in user's ID
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var customerId))
        {
            _logger.LogWarning("Could not retrieve or parse the user ID.");
            ModelState.AddModelError(string.Empty, "Could not retrieve or parse the user ID.");
            return Page();
        }

        CustomerId = customerId;

        // Fetch pet services
        var serviceResult = await _petServiceService.GetAllPetServicesAsync();
        if (serviceResult.IsSuccess && serviceResult.Object is List<PetService> petServices)
        {
            PetServices = petServices;
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
        if (!ModelState.IsValid)
        {
            // Reload necessary data for the page in case of validation errors
            await OnGetAsync();
            return Page();
        }

        // Prepare the request for creating an appointment
        var request = new MakeAppointmentForServiceRequest
        {
            CustomerId = CustomerId,
            PetId = PetId,
            PetServiceId = PetServiceId,
            VeterinarianIds = VeterinarianIds,
            AppointmentDate = AppointmentDate
        };

        // Attempt to create the appointment
        var result = await _appointmentService.MakeAppointmentForServiceAsync(request);
        if (!result.IsSuccess)
        {
            // Log and add errors to the ModelState for display in the view
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                _logger.LogError("Appointment creation failed for customer ID: {CustomerId}. Error: {Error}", CustomerId, error.Description);
            }

            // Reload necessary data for the page and return to the form
            await OnGetAsync();
            return Page();
        }

        // Log success and redirect to the appointment index page
        _logger.LogInformation("Appointment successfully created for customer ID: {CustomerId}", CustomerId);
        return RedirectToPage("Index");
    }

}
                                                                                        

}
