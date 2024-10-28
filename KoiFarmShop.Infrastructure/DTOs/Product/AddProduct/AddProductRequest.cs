using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Product.AddProduct
{
    public class AddProductRequest
    {
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; } // Optional Discount Price
        public int StockQuantity { get; set; }
        public IFormFile ImageFile { get; set; } // Image file for upload
        public DateTime ReleaseDate { get; set; } // Optional Release Date
        public string SKU { get; set; } // Optional Stock Keeping Unit
        public string Manufacturer { get; set; } // Optional Manufacturer
        public string ProductDimensions { get; set; } // Optional Product Dimensions
        public decimal Weight { get; set; } // Optional Weight

        public bool IsFeatured { get; set; }
    }
}
