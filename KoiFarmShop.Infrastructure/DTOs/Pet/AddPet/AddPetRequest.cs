using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Pet.AddPet
{
    public class AddPetRequest
    {
        public Guid PetId { get; set; }
        public string PetName { get; set; }
        public double Age { get; set; }
        public string Color { get; set; }
    }
}
