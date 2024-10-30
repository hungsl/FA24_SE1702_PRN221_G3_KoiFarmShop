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
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceLogic _petServiceLogic;

        public CreateModel(IAppointmentService appointmentService, IPetServiceService petServiceService, IPetServiceLogic petServiceLogic)
        {
            _appointmentService = appointmentService;
            _petServiceService = petServiceService;
            _petServiceLogic = petServiceLogic;
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

        public async Task<IActionResult> OnGetAsync()
        {
            // Get the logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId, out var customerId))
            {
                CustomerId = customerId;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Could not identify the logged-in user.");
                return Page();
            }

            // Fetch pet services
            var serviceResult = await _petServiceService.GetAllPetServicesAsync();
            if (serviceResult.IsSuccess && serviceResult.Object is List<PetService> petServices)
            {
                PetServices = petServices;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to load pet services.");
            }

            // Fetch pets for this customer based on their ID
            var petResult = await _petServiceLogic.GetPetsByOwnerIdAsync(CustomerId);
            if (petResult.IsSuccess && petResult.Object is List<Pet> pets)
            {
                Pets = pets;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No pets found for this customer.");
            }

            return Page();
        }



        // New handler for getting pets by CustomerId
        public async Task<JsonResult> OnGetPetsByCustomerIdAsync(Guid customerId)
        {
            var result = await _petServiceLogic.GetPetsByOwnerIdAsync(customerId);

            if (!result.IsSuccess || result.Object == null)
            {
                return new JsonResult(new { success = false, message = "No pets found for this customer." });
            }

            Pets = result.Object as List<Pet>;
            return new JsonResult(new { success = true, pets = Pets });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new MakeAppointmentForServiceRequest
            {
                CustomerId = CustomerId,
                PetId = PetId,
                PetServiceId = PetServiceId,
                VeterinarianIds = VeterinarianIds,
                AppointmentDate = AppointmentDate
            };

            var result = await _appointmentService.MakeAppointmentForServiceAsync(request);
            if (!result.IsSuccess)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            return RedirectToPage("Index");
        }
    }

}
