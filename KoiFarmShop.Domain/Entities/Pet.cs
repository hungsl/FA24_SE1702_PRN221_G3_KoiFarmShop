using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Entities
{
    public class Pet : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public string Color { get; set; }
        public double Length { get; set; }
        public double Weight { get; set; }
        public DateTime LastHealthCheck { get; set; }
        public int HealthStatus { get; set; }

        // Foreign key relationship
        public Guid OwnerId { get; set; }
        public User Owner { get; set; } // Reference to the User

        // Thêm quan hệ với Appointment
        public Guid PetTypeId { get; set; }
        public PetType PetType { get; set; } // Reference to PetType
        public ICollection<Appointment> Appointments { get; set; } // Quan hệ nhiều với Appointment

    }
}
