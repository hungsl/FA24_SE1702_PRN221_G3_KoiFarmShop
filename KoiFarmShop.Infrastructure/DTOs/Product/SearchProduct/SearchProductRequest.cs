using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Product.SearchProduct
{
    public class SearchProductRequest
    {
        public string? FilterOn { get; set; } // Field to filter on (e.g., Name, Description, Category)
        public string? FilterQuery { get; set; } // Value to filter
        public decimal? FromPrice { get; set; } // Minimum price
        public decimal? ToPrice { get; set; } // Maximum price
        public string? SortBy { get; set; } // Field to sort by (e.g., Name, Price)
        public bool? IsAscending { get; set; } // Sort order
        public int PageNumber { get; set; } = 1; // Current page number
        public int PageSize { get; set; } = 5; // Number of items per page
    }
}
