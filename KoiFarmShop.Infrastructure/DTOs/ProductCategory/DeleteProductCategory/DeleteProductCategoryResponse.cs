using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.ProductCategory.DeleteProductCategory
{
    public class DeleteProductCategoryResponse
    {
        public Guid ProductCategoryId { get; set; }
        public string Name { get; set; }
    }
}
