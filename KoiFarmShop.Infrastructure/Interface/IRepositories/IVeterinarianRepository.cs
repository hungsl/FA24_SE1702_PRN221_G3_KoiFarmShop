using KoiFarmShop.Domain.Entities;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories;

public interface IVeterinarianRepository
{
    Task<List<Veterinarian>> GetAllVeterinariansAsync();

}