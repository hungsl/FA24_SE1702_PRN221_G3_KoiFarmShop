using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.PetService;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Interface.IService
{
    public interface IPetServiceService
    {
        public Task<Result> CreatePetServiceAsync(AddPetServiceRequest petServiceValidator);
        public Task<Result> GetAllPetServicesAsync();
        public Task<Result> GetPetServiceByIdAsync(Guid id);
        public Task<Result> UpdatePetServiceAsync(Guid id, AddPetServiceRequest addPetService);
        public Task<Result> DeletePetServiceAsync(Guid id);
        Task<List<PetService>> GetServicesExpiringSoonAsync();
        public Task<Result> UpdatePetServiceAsync(PetService service);
        Task<List<ServiceFrequency>> GetTopServicesAsync();
        Task<Result> GetAllPetServicesAsync(string searchName, string searchDuration, string searchCategoryName, int pageIndex, int pageSize);
    }
}
