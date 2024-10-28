using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KVSC.Domain.Entities;
using KVSC.Application.Interface.IService;
using KVSC.Infrastructure.DTOs.Product.AddProduct;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicine
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public CreateModel(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        public IActionResult OnGet()
        {
            // Fetch the product categories to display in the dropdown
            var productCategories = _productCategoryService.GetAllProductCategoriesAsync().Result.Object as List<ProductCategory>;
            ViewData["ProductCategoryId"] = new SelectList(productCategories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public AddProductRequest Product { get; set; } = default!;

        // Handle the form submission
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the product service to create the product
            var result = await _productService.CreateProductAsync(Product);

            if (!result.IsSuccess)
            {
                // Handle the failure (optional, you can add more specific error handling)
                ModelState.AddModelError(string.Empty, "Failed to create the product.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
