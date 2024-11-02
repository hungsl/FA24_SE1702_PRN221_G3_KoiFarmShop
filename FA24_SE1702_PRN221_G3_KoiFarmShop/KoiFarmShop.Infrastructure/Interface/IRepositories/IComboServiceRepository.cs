using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IComboServiceRepository : IGenericRepository<ComboService>
    {
        public Task<ComboService> CreateAsync(ComboService comboService);
        public Task<IEnumerable<ComboService>> GetAllComboAsync();

        public Task<ComboService> GetComboByIdAsync(Guid id);
        public Task<int> UpdateAsync(ComboService comboService);

        public Task<int> DeleteAsync(Guid id);

    }
}
