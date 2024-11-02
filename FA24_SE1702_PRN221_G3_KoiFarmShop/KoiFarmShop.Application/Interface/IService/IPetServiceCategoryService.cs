using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IPetServiceCategoryService
    {
        public Task<Result> CreatePetServiceCategoryAsync(AddPetServiceCategoryRequest addPetServiceCategory);


        public Task<Result> GetAllPetServiceCategoriesAsync();


        public Task<Result> GetPetServiceCategoryByIdAsync(Guid id);


        public Task<Result> UpdatePetServiceCategoryAsync(Guid id, AddPetServiceCategoryRequest addPetServiceCategory);
        public Task<Result> DeletePetServiceCategoryAsync(Guid id);

    }
}
