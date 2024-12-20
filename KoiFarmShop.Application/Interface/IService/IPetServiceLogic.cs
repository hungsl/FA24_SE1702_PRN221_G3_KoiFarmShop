﻿using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
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
        Task<Result> GetAllPetAsync();
        Task<Result> CreatePetAsync(AddPetRequest addPet);
        Task<Result> UpdatePetAsync(Guid id, AddPetRequest updatePet);
        Task<Result> DeletePetAsync(Guid id);
        Task<Result> GetSearchPetAsync(string searchName, string searchColor, string searchNote, int pageIndex, int pageSize);
        Task<Result> GetAllOwnerAsync();
        Task<Result> GetOwnerByIdAsync(Guid? id);

        public Task<Result> GetPetsByOwnerIdAsync(Guid ownerId);
    }
}
