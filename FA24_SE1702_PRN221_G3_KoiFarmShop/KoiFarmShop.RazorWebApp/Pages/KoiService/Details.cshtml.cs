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

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class DetailsModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceCategoryService _petCategoryService;
        public DetailsModel(IPetServiceService petServiceService, IPetServiceCategoryService petCategoryService)
        {
            _petServiceService = petServiceService;
            _petCategoryService = petCategoryService;
        }

        public PetService PetService { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _petServiceService.GetPetServiceByIdAsync(id);
            if (result.IsSuccess)
            {
                PetService = result.Object as PetService;
                var PetType = await _petCategoryService.GetPetServiceCategoryByIdAsync(PetService.PetServiceCategoryId);
                var PetCategory = PetType.Object as PetServiceCategory;
                PetService.PetServiceCategory = PetCategory;
            }
            else
            {
                PetService = new PetService();
            }
           
            return Page();
        }
    }
}
