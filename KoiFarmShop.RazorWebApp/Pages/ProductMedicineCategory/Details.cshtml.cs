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
    public class DetailsModel : PageModel
    {
        private readonly KoiFarmShop.Infrastructure.DB.KVSCContext _context;

        public DetailsModel(KoiFarmShop.Infrastructure.DB.KVSCContext context)
        {
            _context = context;
        }

        public ProductCategory ProductCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productcategory = await _context.ProductCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (productcategory == null)
            {
                return NotFound();
            }
            else
            {
                ProductCategory = productcategory;
            }
            return Page();
        }
    }
}
