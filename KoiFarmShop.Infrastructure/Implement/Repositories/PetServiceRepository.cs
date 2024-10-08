using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Implement.Repositories
{
    public class PetServiceRepository : GenericRepository<PetService>, IPetServiceRepository
    {
        public PetServiceRepository(KVSCContext context) : base(context) { }

        // CREATE
        public async Task<PetService> CreateServiceAsync(PetService petService)
        {
            _context.PetServices.Add(petService);
            await _context.SaveChangesAsync();
            return petService;
        }

        // READ
        public async Task<IEnumerable<PetService>> GetAllServicesAsync()
        {
            return await _context.PetServices.Include(i => i.PetServiceCategory).Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<(int totalItems, List<PetService> petServices)> GetAllServiceWithSearchAsync(string searchTerm, int pageIndex, int pageSize)
        {
            var query = _context.Set<PetService>().AsQueryable(); ;

            // Tìm kiếm theo tên dịch vụ
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            // Phân trang
            var totalItems = await query.CountAsync();
            var petServices = await query
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return (totalItems, petServices);
        }

        public async Task<PetService> GetServiceByIdAsync(Guid id)
        {
            return await _context.PetServices.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }
        //get by list id
        public async Task<List<PetService>> GetByIdsAsync(List<Guid> serviceIds)
        {
            return await _context.PetServices
                                 .Where(ps => serviceIds.Contains(ps.Id))
                                 .ToListAsync();
        }
        public async Task<int> GetServiceByPetServiceCategoryIdAsync(Guid id)
        {
            return await _context.PetServices.CountAsync(s => s.PetServiceCategoryId == id && !s.IsDeleted);
        }


        // DELETE (soft delete)
        public async Task<int> DeleteServiceAsync(Guid id)
        {
            var service = await _context.PetServices.FindAsync(id);
            if (service != null)
            {
                service.IsDeleted = true;
                _context.PetServices.Update(service);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

       
    }
}
