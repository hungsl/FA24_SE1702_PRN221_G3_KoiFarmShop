using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KVSC.Application.Interface.IService;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using System;
using System.Threading.Tasks;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Product.UpdateProduct;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicine
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public EditModel(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        [BindProperty]
        public UpdateProductRequest Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _productService.GetProductByIdAsync(id);
            if (!result.IsSuccess || result.Object == null)
            {
                return NotFound();
            }

            var existingProduct = result.Object as Product;

            Product = new UpdateProductRequest
            {
                Id = existingProduct.Id, // Ensure the Id is populated
                ProductCategoryId = existingProduct.ProductCategoryId,
                Name = existingProduct.Name,
                Description = existingProduct.Description,
                Price = existingProduct.Price,
                DiscountPrice = existingProduct.DiscountPrice,
                StockQuantity = existingProduct.StockQuantity,
                ReleaseDate = existingProduct.ReleaseDate,
                SKU = existingProduct.SKU,
                Manufacturer = existingProduct.Manufacturer,
                ProductDimensions = existingProduct.ProductDimensions,
                Weight = existingProduct.Weight,
                IsFeatured = existingProduct.IsFeatured,
                ImageFile = null // No file selected initially
            };

            // Fetch the categories for dropdown
            var categoriesResult = await _productCategoryService.GetAllProductCategoriesAsync();
            if (categoriesResult.IsSuccess)
            {
                var productCategories = categoriesResult.Object as List<ProductCategory>;
                ViewData["ProductCategoryId"] = new SelectList(productCategories, "Id", "Name");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            

            var result = await _productService.UpdateProductAsync(id, Product);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, "Failed to update product");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
