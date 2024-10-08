using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Pet.AddPetType
{
    public class AddPetTypeRequest
    {
        public string GeneralType { get; set; }
        public string SpecificType { get; set; }
        public Guid PetHabitatId { get; set; }
    }
}
