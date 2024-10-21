using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Product.GetProduct
{
    public class GetProductResponse
    {
        public Guid Id { get; set; }            // Unique identifier for the product
        public string? Name { get; set; }        // Name of the product
        public string? Description { get; set; } // Description of the product
        public decimal Price { get; set; }      // Price of the product
        public int StockQuantity { get; set; }  // Stock quantity of the product
        public Guid ProductCategoryId { get; set; } // Product's category identifier
        //public string? ProductCategoryName { get; set; } // Optional: category name

        // Property to hold the image data as MemoryStream
        public string? ImageStream { get; set; }
    }
}
