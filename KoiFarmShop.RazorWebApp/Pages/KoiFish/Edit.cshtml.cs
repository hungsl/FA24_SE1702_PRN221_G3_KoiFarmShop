using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class EditModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public EditModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _petServiceLogic.GetPetByIdAsync(id);
            if (!result.IsSuccess)
            {
                return NotFound();
            }

            Pet = result.Object as Pet;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var addPetRequest = new AddPetRequest
            {
                Name = Pet.Name,
                Age = Pet.Age,
                Gender = Pet.Gender,
                ImageUrl = Pet.ImageUrl,
                Color = Pet.Color,
                Length = Pet.Length,
                Weight = Pet.Weight,
                Quantity = Pet.Quantity,
                LastHealthCheck = Pet.LastHealthCheck,
                Note = Pet.Note,
                HealthStatus = Pet.HealthStatus
            };

            await _petServiceLogic.UpdatePetAsync(Pet.Id, addPetRequest);
            return RedirectToPage("./Index");
        }

        private bool PetExists(Guid id)
        {
            var petService = _petServiceLogic.GetPetByIdAsync(id);
            return petService != null;
        }
    }
}
