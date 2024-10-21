using KVSC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Entities
{
    public class PetService : BaseEntity
    {
        public string Name { get; set; } // Name of the specific service
        public Guid PetServiceCategoryId { get; set; } // Foreign key to ServiceCategory
        public decimal BasePrice { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public decimal TravelCost { get; set; }

        // Relationships
        public PetServiceCategory PetServiceCategory { get; set; } // Navigation property
        public ICollection<OrderItem> OrderItems { get; set; }
        // New relationship: ComboServiceItems (for many-to-many relation with ComboService)
        public ICollection<ComboServiceItem> ComboServiceItems { get; set; }

    }
}
