using KoiFarmShop.Domain.Entities;

namespace KVSC.Domain.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; } // Foreign key to User
    public User Customer { get; set; }

    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; }

    // Relationships
    public ICollection<OrderItem> OrderItems { get; set; }
}