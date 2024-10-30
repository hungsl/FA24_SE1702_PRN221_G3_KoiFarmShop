using KoiFarmShop.Application.Common.Result;

namespace KoiFarmShop.Application.Interface.IService;

public interface IVeterinarianService
{
    Task<Result> GetAllVeterinariansAsync();

}