using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Application.Interface.IService;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class DeleteModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public DeleteModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

        [BindProperty]
        public Pet Pet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _petServiceLogic.GetPetByIdAsync(id);
            if (result.IsSuccess)
            {
                Pet = result.Object as Pet;
            }
            else
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var result = await _petServiceLogic.GetPetByIdAsync(id);
            if (result.IsSuccess)
            {
                Pet = result.Object as Pet;
                await _petServiceLogic.DeletePetAsync(Pet.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
