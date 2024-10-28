using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace KoiFarmShop.RazorWebApp.Pages.KoiAppointment
{
    public class CreateModel : PageModel
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPetServiceService _petServiceService;

        public CreateModel(IAppointmentService appointmentService, IPetServiceService petServiceService)
        {
            _appointmentService = appointmentService;
            _petServiceService = petServiceService;
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

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _petServiceService.GetAllPetServicesAsync();

            if (result.IsSuccess && result.Object is List<PetService> petServices)
            {
                PetServices = petServices;
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to load pet services.");
            }

            return Page();
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
