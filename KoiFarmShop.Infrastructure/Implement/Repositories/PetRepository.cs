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
    public class PetRepository : GenericRepository<Pet>, IPetRepository
    {

        public PetRepository(KVSCContext context) : base(context) { }
        public async Task<Pet> GetPetByIdAsync(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<List<Pet>> GetAllPetAsync()
        {
            return await _context.Pets.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Pet> CreatePetAsync(Pet pet)
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return pet;
        }

        public async Task<int> UpdatePetAsync(Pet pet)
        {
            _context.Pets.Update(pet);
            return await _context.SaveChangesAsync();
        }

        //xoa luon pet
        public async Task<int> DeletePetAsync(Guid id)
        {
            var pet = await GetPetByIdAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        //xoa nhe pet
        public async Task<int> SoftDeletePetAsync(Guid id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                pet.IsDeleted = true;
                _context.Pets.Update(pet);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

    }
}
