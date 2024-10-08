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
        Task<Pet> GetPetByIdAsync(Guid id);
        Task<List<Pet>> GetAllPetAsync();
        Task<Pet> CreatePetAsync(Pet pet);
        Task<int> UpdatePetAsync(Pet pet);
        Task<int> DeletePetAsync(Guid id);
        Task<int> SoftDeletePetAsync(Guid id);
    }
}
