using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KVSC.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;

namespace KoiFarmShop.RazorWebApp.Pages.ProductMedicine
{
    public class CreateModel : PageModel
    {
        private readonly KoiFarmShop.Infrastructure.DB.KVSCContext _context;

        public CreateModel(KoiFarmShop.Infrastructure.DB.KVSCContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
