using KoiFarmShop.Application.Common.Result;
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
        public Task<Result> GetAllPetServicesAsync(string searchTerm, int pageIndex, int pageSize);
        public Task<Result> GetPetServiceByIdAsync(Guid id);
        public Task<Result> UpdatePetServiceAsync(Guid id, AddPetServiceRequest addPetService);
        public Task<Result> DeletePetServiceAsync(Guid id);
    }
}
