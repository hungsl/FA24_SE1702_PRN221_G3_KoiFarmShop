using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPetType;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePetType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IPetTypeService
    {
        Task<Result> GetPetTypeByIdAsync(Guid id);
        Task<Result> GetAllPetTypesAsync();
        Task<Result> CreatePetTypeAsync(AddPetTypeRequest addPetTypeDto);
        Task<Result> UpdatePetTypeAsync(Guid id, UpdatePetTypeRequest updatePetTypeDto);
        Task<Result> DeletePetTypeAsync(Guid id);
    }
}
