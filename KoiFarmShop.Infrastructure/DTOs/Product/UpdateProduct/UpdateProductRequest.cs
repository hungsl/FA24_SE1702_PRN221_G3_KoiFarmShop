using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Product.UpdateProduct
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; } // Product Id
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; } // Optional Discount Price
        public string? Description { get; set; }
        public int? StockQuantity { get; set; }
        public string? ImageUrl { get; set; } // URL for the image (if updating)
        public DateTime? ReleaseDate { get; set; } // Optional Release Date
        public string? SKU { get; set; } // Optional SKU
        public string? Manufacturer { get; set; } // Optional Manufacturer
        public string? ProductDimensions { get; set; } // Optional Product Dimensions
        public decimal? Weight { get; set; } // Optional Weight
        public Guid? ProductCategoryId { get; set; }

        public bool? IsFeatured { get; set; } // IsFeatured field to mark a product as featured
        public IFormFile? ImageFile { get; set; }
    }
}
