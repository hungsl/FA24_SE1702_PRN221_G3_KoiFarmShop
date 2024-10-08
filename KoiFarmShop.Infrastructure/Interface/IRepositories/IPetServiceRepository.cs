using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IPetServiceRepository : IGenericRepository<PetService>
    {
        public Task<PetService> CreateServiceAsync(PetService petService);
        public Task<IEnumerable<PetService>> GetAllServicesAsync();
        public Task<PetService> GetServiceByIdAsync(Guid id);
        public Task<int> DeleteServiceAsync(Guid id);
        public Task<int> GetServiceByPetServiceCategoryIdAsync(Guid id);
        public Task<List<PetService>> GetByIdsAsync(List<Guid> serviceIds);
        public Task<(int totalItems, List<PetService> petServices)> GetAllServiceWithSearchAsync(string searchTerm, int pageIndex, int pageSize);
    }
}
