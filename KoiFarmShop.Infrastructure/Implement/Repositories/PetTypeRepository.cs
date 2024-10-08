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
    public class PetTypeRepository : GenericRepository<PetType>, IPetTypeRepository
    {
        public PetTypeRepository(KVSCContext context) : base(context)
        {
        }

        public async Task<PetType> GetPetTypeByIdAsync(Guid id)
        {
            return await _context.PetTypes.FirstOrDefaultAsync(pt => pt.Id == id && !pt.IsDeleted);
        }

        public async Task<List<PetType>> GetAllPetTypesAsync()
        {
            return await _context.PetTypes.Where(pt => !pt.IsDeleted).ToListAsync();
        }

        public async Task<PetType> CreatePetTypeAsync(PetType petType)
        {
            _context.PetTypes.Add(petType);
            await _context.SaveChangesAsync();
            return petType;
        }

        public async Task<int> UpdatePetTypeAsync(PetType petType)
        {
            _context.PetTypes.Update(petType);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePetTypeAsync(Guid id)
        {
            var petType = await GetPetTypeByIdAsync(id);
            if (petType != null)
            {
                _context.PetTypes.Remove(petType);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> SoftDeletePetTypeAsync(Guid id)
        {
            var petType = await _context.PetTypes.FindAsync(id);
            if (petType != null)
            {
                petType.IsDeleted = true;
                _context.PetTypes.Update(petType);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
