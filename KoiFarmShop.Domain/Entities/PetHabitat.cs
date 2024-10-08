using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Domain.Entities
{
    public class PetHabitat : BaseEntity
    {
        public string HabitatType { get; set; }

        // Foreign key relationship
        public ICollection<PetType> PetTypes { get; set; }

    }
}
