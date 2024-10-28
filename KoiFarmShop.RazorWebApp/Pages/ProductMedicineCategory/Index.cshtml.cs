using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KVSC.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicineCategory
{
    public class IndexModel : PageModel
    {
        private readonly KoiFarmShop.Infrastructure.DB.KVSCContext _context;

        public IndexModel(KoiFarmShop.Infrastructure.DB.KVSCContext context)
        {
            _context = context;
        }

        public IList<ProductCategory> ProductCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ProductCategory = await _context.ProductCategories.ToListAsync();
        }
    }
}
