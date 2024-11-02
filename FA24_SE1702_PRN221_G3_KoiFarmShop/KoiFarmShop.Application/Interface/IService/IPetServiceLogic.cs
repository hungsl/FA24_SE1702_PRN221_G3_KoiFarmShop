using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IPetServiceLogic
    {
        Task<Result> GetPetByIdAsync(Guid id);
        Task<Result> GetAllPetAsync();
        Task<Result> CreatePetAsync(AddPetRequest addPet);
        Task<Result> UpdatePetAsync(Guid id, AddPetRequest updatePet);
        Task<Result> DeletePetAsync(Guid id);
        Task<Result> GetSearchPetAsync(string searchName, string searchColor, string searchNote);

        public Task<Result> GetPetsByOwnerIdAsync(Guid ownerId);
    }
}
