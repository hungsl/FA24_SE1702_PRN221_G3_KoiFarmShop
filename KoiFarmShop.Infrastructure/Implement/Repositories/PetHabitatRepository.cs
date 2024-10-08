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
    public class PetHabitatRepository : GenericRepository<PetHabitat>, IPetHabitatRepository
    {
        public PetHabitatRepository(KVSCContext context) : base(context)
        {
        }

        public async Task<PetHabitat> GetPetHabitatByIdAsync(Guid id)
        {
            return await _context.PetHabitats.FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);
        }
        public async Task<List<PetHabitat>> GetAllPetHabitatsAsync()
        {
            return await _context.PetHabitats.Where(h => !h.IsDeleted).ToListAsync();
        }

        public async Task<PetHabitat> CreatePetHabitatAsync(PetHabitat petHabitat)
        {
            _context.PetHabitats.Add(petHabitat);
            await _context.SaveChangesAsync();
            return petHabitat;
        }

        public async Task<int> UpdatePetHabitatAsync(PetHabitat petHabitat)
        {
            _context.PetHabitats.Update(petHabitat);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeletePetHabitatAsync(Guid id)
        {
            var petHabitat = await GetPetHabitatByIdAsync(id);
            if (petHabitat != null)
            {
                _context.PetHabitats.Remove(petHabitat);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
