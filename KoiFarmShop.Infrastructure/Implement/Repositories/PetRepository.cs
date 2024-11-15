using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> Dev_Danh_skibidi

namespace KoiFarmShop.Infrastructure.Implement.Repositories
{
    public class PetRepository : GenericRepository<Pet>, IPetRepository
    {

        public PetRepository(KVSCContext context) : base(context) { }

<<<<<<< HEAD
        // Get all pets by OwnerId
        public async Task<IEnumerable<Pet>> GetPetsByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Pets
                .Where(p => p.OwnerId == ownerId && !p.IsDeleted)
                .ToListAsync();
        }

=======
>>>>>>> Dev_Danh_skibidi
        public async Task<Pet> GetPetByIdAsync(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<Pet>> GetAllPetAsync()
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

<<<<<<< HEAD
        public async Task<List<Pet>> GetAllPetWithSearchAsync(string searchName, string searchColor, string searchNote)
        {
            var query = _context.Set<Pet>().AsQueryable().Where(p => !p.IsDeleted);
=======
        public async Task<(int totalItems, List<Pet> Pets)> GetAllPetWithSearchAsync(
        string searchName, string searchColor, string searchNote,
        int pageIndex, int pageSize)
        {
            var query = _context.Set<Pet>()
                .Include(i => i.Owner)
                .AsQueryable().Where(p => !p.IsDeleted);
>>>>>>> Dev_Danh_skibidi

            // Tìm kiếm theo tên dịch vụ
            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(p => p.Name.Contains(searchName));
            }

            // Tìm kiếm theo thời gian dịch vụ
            if (!string.IsNullOrEmpty(searchColor))
            {
                query = query.Where(p => p.Color.Contains(searchColor));
            }

            // Tìm kiếm theo tên danh mục dịch vụ
            if (!string.IsNullOrEmpty(searchNote))
            {
                query = query.Where(p => p.Note.Contains(searchNote));
            }

<<<<<<< HEAD
            return await query.ToListAsync();
=======
            // Phân trang
            var totalItems = await query.CountAsync();
            var pet = await query
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (totalItems, pet);
        }

        public async Task<List<User>> GetAllOwnerAsync()
        {
            return await _context.Users
                                 .Where(u => u.role == 5)
                                 .ToListAsync();
        }

        public async Task<User> GetOwnerByIdAsync(Guid? id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
>>>>>>> Dev_Danh_skibidi
        }
    }
}
