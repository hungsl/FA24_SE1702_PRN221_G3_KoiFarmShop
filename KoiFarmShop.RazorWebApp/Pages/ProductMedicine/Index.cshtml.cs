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
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<Product> Product { get;set; } = default!;

        //public async Task OnGetAsync()
        //{
        //    Product = await _productService.Products
        //        .Include(p => p.ProductCategory).ToListAsync();
        //}
    }
}
