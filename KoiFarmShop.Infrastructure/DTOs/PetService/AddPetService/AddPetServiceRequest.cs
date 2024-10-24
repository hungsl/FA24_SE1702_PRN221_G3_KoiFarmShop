using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService
{
    public class AddPetServiceRequest
    {
        public string Name { get; set; }
        public Guid PetServiceCategoryId { get; set; }
        public decimal BasePrice { get; set; }
        public string Duration { get; set; }
        public string ImageUrl { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public decimal TravelCost { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfPets { get; set; }

    }
}
