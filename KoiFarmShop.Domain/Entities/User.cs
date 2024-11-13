using KVSC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int role { get; set; }//1 admin, 2 manager,3 veterinarian, 4 staff, 5 customer

        // Relationships
        public ICollection<Pet> Pets { get; set; }
        public Veterinarian Veterinarian { get; set; }
        public ICollection<AppointmentVeterinarian> AppointmentVeterinarians { get; set; }//dành cho veterinarians

        // Thêm quan hệ với Appointment
        public ICollection<Appointment> Appointments { get; set; } // Quan hệ nhiều với Appointment
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
