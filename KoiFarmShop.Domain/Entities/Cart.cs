using KoiFarmShop.Domain.Entities;

namespace KVSC.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid CustomerId { get; set; } // Foreign key to User
    public User Customer { get; set; }

    // Relationships
    public ICollection<CartItem> CartItems { get; set; }
}
