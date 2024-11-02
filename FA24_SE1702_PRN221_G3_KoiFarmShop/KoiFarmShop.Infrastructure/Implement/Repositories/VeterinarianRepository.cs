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
        return await _context.Veterinarians
            .Include(v => v.User) // Ensure User data is loaded with Veterinarian
            .ToListAsync();
    }



}