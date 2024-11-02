using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Implement.Repositories
{
    public class PetServiceCategoryRepository : GenericRepository<PetServiceCategory>, IPetServiceCategoryRepository
    {
        public PetServiceCategoryRepository(KVSCContext context) : base(context) { }

        // Kiểm tra xem danh mục dịch vụ có tồn tại không

        public async Task<PetServiceCategory> CreateAsync(PetServiceCategory category)
        {
            await _context.PetServiceCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            var category = await _context.PetServiceCategories.FindAsync(id);
            if (category != null)
            {
                _context.PetServiceCategories.Remove(category);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
