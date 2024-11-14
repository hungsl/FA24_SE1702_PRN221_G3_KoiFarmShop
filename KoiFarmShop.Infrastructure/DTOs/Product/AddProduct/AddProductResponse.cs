using KoiFarmShop.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Product.AddProduct
{
    public class AddProductResponse
    {
        public int? CategoryId { get; set; }
        public Error Error { get; set; } = Error.None;
    }
}
