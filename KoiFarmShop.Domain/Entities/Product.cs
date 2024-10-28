using KoiFarmShop.Domain.Entities;

namespace KVSC.Domain.Entities;

public class Product : BaseEntity
{
    public Guid ProductCategoryId { get; set; } // Foreign key to ProductCategory
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string ImageUrl { get; set; }

    public decimal DiscountPrice { get; set; } // Discounted price of the product
    public bool IsFeatured { get; set; } // Flag to indicate if the product is featured
    public DateTime ReleaseDate { get; set; } // Release date of the product
    public string SKU { get; set; } // Stock Keeping Unit for inventory tracking
    public string Manufacturer { get; set; } // Manufacturer of the product
    public string ProductDimensions { get; set; } // Dimensions of the product (e.g., WxHxD)
    public decimal Weight { get; set; } // Weight of the product

    // Relationships
    public ICollection<OrderItem> OrderItems { get; set; }
    public ProductCategory ProductCategory { get; set; } // Navigation property to ProductCategory
}