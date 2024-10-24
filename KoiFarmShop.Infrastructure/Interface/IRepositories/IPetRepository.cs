using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IPetRepository : IGenericRepository<Pet>
    {
        public Task<Pet> GetPetByIdAsync(Guid id);
        public Task<IEnumerable<Pet>> GetAllPetAsync();
        public Task<Pet> CreatePetAsync(Pet pet);
        public Task<int> UpdatePetAsync(Pet pet);
        public Task<int> DeletePetAsync(Guid id);
    }
}
