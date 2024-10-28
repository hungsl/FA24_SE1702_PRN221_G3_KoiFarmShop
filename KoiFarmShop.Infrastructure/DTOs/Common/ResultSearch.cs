using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Common
{
    public class ResultSearch<T>
    {
        public List<T> Items { get; set; }
    }
}
