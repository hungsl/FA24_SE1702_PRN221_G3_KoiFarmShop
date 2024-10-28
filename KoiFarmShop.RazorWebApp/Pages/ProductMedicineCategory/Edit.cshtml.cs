using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KVSC.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicineCategory
{
    public class EditModel : PageModel
    {
        private readonly KoiFarmShop.Infrastructure.DB.KVSCContext _context;

        public EditModel(KoiFarmShop.Infrastructure.DB.KVSCContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductCategory ProductCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productcategory =  await _context.ProductCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (productcategory == null)
            {
                return NotFound();
            }
            ProductCategory = productcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(ProductCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductCategoryExists(Guid id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
