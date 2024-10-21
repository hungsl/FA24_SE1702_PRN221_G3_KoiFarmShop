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

    // Relationships
    public ICollection<OrderItem> OrderItems { get; set; }
    public ProductCategory ProductCategory { get; set; } // Navigation property to ProductCategory
}