using KoiFarmShop.Domain.Entities;

namespace KVSC.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; } // Foreign key to Order
    public Order Order { get; set; }

    public Guid? PetId { get; set; } // Optional foreign key to Pet
    public Pet Pet { get; set; }

    public Guid? ServiceId { get; set; } // Optional foreign key to Service
    public PetService PetService { get; set; }

    public Guid? ProductId { get; set; } // Optional foreign key to Product
    public Product Product { get; set; }

    public Guid? VeterinarianId { get; set; } // Optional foreign key to Veterinarian
    public User Veterinarian { get; set; }

    public decimal Price { get; set; }
    public decimal TravelCost { get; set; }
    public string Location { get; set; }
    public string Status { get; set; }
    public DateTime? AppointmentDateTime { get; set; } // Nullable for product-only orders
}
