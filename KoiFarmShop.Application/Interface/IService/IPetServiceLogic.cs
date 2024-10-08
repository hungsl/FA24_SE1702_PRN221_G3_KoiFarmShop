using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IPetServiceLogic
    {
        Task<Result> GetPetByIdAsync(Guid id);
        Task<Result> CreatePetAsync(AddPetRequest addPet);
        Task<Result> UpdatePetAsync(Guid id, UpdatePetRequest updatePet);
        Task<Result> DeletePetAsync(Guid id);
        Task<Result> GetAllPetAsync();
    }
}
