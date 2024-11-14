using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KVSC.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KVSC.Application.Interface.IService;
using KVSC.Infrastructure.DTOs.ProductCategory.AddProductCategory;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicineCategory
{
    public class CreateModel : PageModel
    {
        private readonly IProductCategoryService _productCategoryService;

        public CreateModel(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AddProductCategoryRequest ProductCategory { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Create a new product category using the service
            var result = await _productCategoryService.AddProductCategoryAsync(ProductCategory);

            if (!result.IsSuccess)
            {
                // Handle failure (optional)
                ModelState.AddModelError(string.Empty, "Failed to create the product category.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
