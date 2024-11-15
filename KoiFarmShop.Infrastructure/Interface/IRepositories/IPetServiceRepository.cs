﻿using KoiFarmShop.Domain.Entities;
<<<<<<< HEAD
using KoiFarmShop.Infrastructure.DTOs.PetService;
=======
>>>>>>> Dev_Danh_skibidi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.Interface.IRepositories
{
    public interface IPetServiceRepository : IGenericRepository<PetService>
    {
        public Task<PetService> CreateServiceAsync(PetService petService);
        public Task<List<PetService>> GetAllServicesAsync();
        public Task<PetService> GetServiceByIdAsync(Guid id);
        public Task<int> DeleteServiceAsync(Guid id);
        public Task<int> GetServiceByPetServiceCategoryIdAsync(Guid id);
        public Task<List<PetService>> GetByIdsAsync(List<Guid> serviceIds);
        Task<(int totalItems, List<PetService> petServices)> GetAllServiceWithSearchAsync(
        string searchName, string searchDuration, string searchCategoryName,
        int pageIndex, int pageSize);
        Task<List<PetService>> GetServicesExpiringSoonAsync();
<<<<<<< HEAD
        Task<List<ServiceFrequency>> GetTopServicesAsync();
=======
>>>>>>> Dev_Danh_skibidi
    }
}
