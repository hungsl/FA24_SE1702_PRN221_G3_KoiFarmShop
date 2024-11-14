using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KVSC.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KVSC.Application.Interface.IService;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicine
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // OnGetAsync to fetch product details before deletion
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch product details by id using the service
            var result = await _productService.GetProductByIdAsync(id);

            if (!result.IsSuccess || result.Object == null)
            {
                return NotFound();
            }

            Product = result.Object as Product;
            return Page();
        }

        // OnPostAsync to confirm the deletion of the product
        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the product by id to ensure it exists
            var result = await _productService.GetProductByIdAsync(id);
            if (result.IsSuccess && result.Object != null)
            {
                await _productService.DeleteProductAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
