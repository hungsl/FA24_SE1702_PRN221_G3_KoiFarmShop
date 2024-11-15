﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class CreateModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public CreateModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

        public IActionResult OnGet()
        {
<<<<<<< HEAD
=======
            var owners = _petServiceLogic.GetAllOwnerAsync().Result.Object as List<User>;
            ViewData["OwnerId"] = new SelectList(owners, "Id", "FullName");
>>>>>>> Dev_Danh_skibidi
            return Page();
        }

        [BindProperty]
        public AddPetRequest Pet { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _petServiceLogic.CreatePetAsync(Pet);
<<<<<<< HEAD
=======
            var owners = _petServiceLogic.GetAllOwnerAsync().Result.Object as List<User>;
            ViewData["OwnerId"] = new SelectList(owners, "Id", "FullName");
>>>>>>> Dev_Danh_skibidi

            if (!result.IsSuccess) 
            {
                ModelState.AddModelError(string.Empty, "Failed to create the pet. Please try again.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}