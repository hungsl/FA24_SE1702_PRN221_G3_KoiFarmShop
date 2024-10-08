using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPetHabitat;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePetHabitat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IPetHabitatService
    {
        Task<Result> GetPetHabitatByIdAsync(Guid id);
        Task<Result> CreatePetHabitatAsync(AddPetHabitatRequest addPetHabitatDto);
        Task<Result> UpdatePetHabitatAsync(Guid id, UpdatePetHabitatRequest updatePetHabitatDto);
        Task<Result> DeletePetHabitatAsync(Guid id);
        Task<Result> GetAllPetHabitatsAsync();
    }
}
