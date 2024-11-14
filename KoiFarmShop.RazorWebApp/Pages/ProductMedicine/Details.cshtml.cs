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
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch product details using the service
            var result = await _productService.GetProductByIdAsync(id);
            if (!result.IsSuccess || result.Object == null)
            {
                return NotFound();
            }

            Product = result.Object as Product;
            return Page();
        }
    }
}
