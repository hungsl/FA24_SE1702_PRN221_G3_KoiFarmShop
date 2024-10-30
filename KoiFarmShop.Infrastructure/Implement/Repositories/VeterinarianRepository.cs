using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Infrastructure.Implement.Repositories;

public class VeterinarianRepository : GenericRepository<User>, IVeterinarianRepository
{
    public VeterinarianRepository(KVSCContext context) : base(context) { }

    public async Task<List<Veterinarian>> GetAllVeterinariansAsync()
    {
        return await _context.Users
            .Where(u => u.role == 3) // Only veterinarians
            .Include(u => u.Veterinarian) // Include the Veterinarian relationship
            .ThenInclude(v => v.VeterinarianSchedules) // Include schedules for each veterinarian
            .Include(u => u.Veterinarian.User) // Ensure User details are included
            .Select(u => u.Veterinarian) // Project to Veterinarian entity
            .ToListAsync();
    }

}