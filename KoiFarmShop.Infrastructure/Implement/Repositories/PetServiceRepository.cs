using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
<<<<<<< HEAD
using KoiFarmShop.Infrastructure.DTOs.PetService;
=======
>>>>>>> Dev_Danh_skibidi
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
        public async Task<List<PetService>> GetAllServicesAsync()
        {
            var result = await _context.PetServices.Include(i => i.PetServiceCategory).Where(s => !s.IsDeleted).ToListAsync();
            return result;
        }

        public async Task<(int totalItems, List<PetService> petServices)> GetAllServiceWithSearchAsync(
        string searchName, string searchDuration, string searchCategoryName,
        int pageIndex, int pageSize)
        {
            var query = _context.Set<PetService>()
                                .Include(i => i.PetServiceCategory) // Bao gồm PetServiceCategory
                                .AsQueryable().Where(p => !p.IsDeleted); 

            // Tìm kiếm theo tên dịch vụ
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(p => p.Name.Contains(searchName));
            }

            // Tìm kiếm theo thời gian dịch vụ
            if (!string.IsNullOrEmpty(searchDuration))
            {
                query = query.Where(p => p.Duration.Contains(searchDuration));
            }

            // Tìm kiếm theo tên danh mục dịch vụ
            if (!string.IsNullOrEmpty(searchCategoryName))
            {
                query = query.Where(p => p.PetServiceCategory.Name.Contains(searchCategoryName));
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
        public async Task<List<PetService>> GetServicesExpiringSoonAsync()
        {
            var oneHourFromNow = DateTime.UtcNow.ToLocalTime().AddHours(1);
            var result =  await _context.PetServices
                .Where(s => !s.IsDeleted &&
                            s.AvailableTo > DateTime.UtcNow && s.AvailableTo <= oneHourFromNow)// check xem dich vu con 1 tieng nua het han
                .ToListAsync();
            return result;
        }
<<<<<<< HEAD
        public async Task<List<ServiceFrequency>> GetTopServicesAsync()
        {
                var topServices = await _context.PetServices
                    .OrderByDescending(s => s.Frequency) // Sắp xếp theo tần suất dịch vụ
                    .Take(5) // Giới hạn số lượng kết quả, ví dụ 5 dịch vụ phổ biến nhất
                    .Select(s => new ServiceFrequency { ServiceName = s.Name, Frequency = s.Frequency })
                    .ToListAsync();

                return topServices;
        }
=======
>>>>>>> Dev_Danh_skibidi

    }
}
