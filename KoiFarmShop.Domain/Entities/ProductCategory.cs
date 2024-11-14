using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Domain.Entities
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; } // Category name, e.g., "Accessories", "Fish Food"
        public string Description { get; set; } // Optional description

        // Navigation property for related Products (if needed)
        public virtual ICollection<Product> Products { get; set; }
    }
}
