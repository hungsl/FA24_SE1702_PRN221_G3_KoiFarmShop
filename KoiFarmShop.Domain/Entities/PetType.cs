using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Entities
{
    public class PetType : BaseEntity
    {
        public string GeneralType { get; set; }
        public string SpecificType { get; set; }

        // Relationship
        public Guid PetHabitatId { get; set; }
        public PetHabitat PetHabitat { get; set; }
        public ICollection<Pet> Pets { get; set; }
    }
}
