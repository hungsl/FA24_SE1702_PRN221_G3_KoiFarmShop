using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class CreateModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceCategoryService _petCategoryService;
        public CreateModel(IPetServiceService petServiceService, IPetServiceCategoryService petCategoryService)
        {
            _petServiceService = petServiceService;
            _petCategoryService = petCategoryService;
        }

        public IActionResult OnGet()
        {
            var PetType =  _petCategoryService.GetAllPetServiceCategoriesAsync().Result.Object as List<PetServiceCategory>;
            ViewData["PetServiceCategoryId"] = new SelectList(PetType, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public AddPetServiceRequest PetService { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _petServiceService.CreatePetServiceAsync(PetService);
            return RedirectToPage("./Index");
        }
    }
}
