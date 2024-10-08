using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IPetTypeRepository
    {
        Task<PetType> GetPetTypeByIdAsync(Guid id);
        Task<List<PetType>> GetAllPetTypesAsync();
        Task<PetType> CreatePetTypeAsync(PetType petType);
        Task<int> UpdatePetTypeAsync(PetType petType);
        Task<int> DeletePetTypeAsync(Guid id);
        Task<int> SoftDeletePetTypeAsync(Guid id);
    }
}
