using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KVSC.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.DTOs.Common;
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

        public PagedResultSearch<Product> Product { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchField { get; set; } = "Name"; // Default to Name search


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinDiscountPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxDiscountPrice { get; set; }

        public async Task OnGetAsync()
        {
            // Call the product service with the additional filter parameters for price and discount price
            var result = await _productService.GetAllProductsAsync(SearchTerm, SearchField, PageIndex, PageSize, MinPrice, MaxPrice, MinDiscountPrice, MaxDiscountPrice);

            if (result.IsSuccess)
            {
                // Cast the result to the PagedResultSearch<Product> type
                Product = result.Object as PagedResultSearch<Product>;
            }
            else
            {
                // In case of failure, initialize an empty result set
                Product = new PagedResultSearch<Product>
                {
                    TotalItems = 0,
                    Items = new List<Product>()
                };
            }
        }
    }
}
