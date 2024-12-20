﻿using Azure;
using FluentValidation;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using System.Linq.Dynamic.Core;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.PetService;

namespace KoiFarmShop.Application.Implement.Service
{
    public class PetServiceService : IPetServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddPetServiceRequest> _petServiceValidator;

        public PetServiceService(IUnitOfWork unitOfWork, IValidator<AddPetServiceRequest> petServiceValidator)
        {
            _unitOfWork = unitOfWork;
            _petServiceValidator = petServiceValidator;
        }

        public async Task<Result> CreatePetServiceAsync(AddPetServiceRequest addPetService)
        {
            // Validate the input
            var validationResult = await _petServiceValidator.ValidateAsync(addPetService);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            var categoryExists = await _unitOfWork.PetServiceCategoryRepository.GetByIdAsync(addPetService.PetServiceCategoryId);
            if (categoryExists == null)
            {
                return Result.Failure(PetServiceErrorMessage.InvalidFieldValue("PetServiceCategory"));
            }

            var petService = new PetService
            {
                Name = addPetService.Name,
                PetServiceCategoryId = addPetService.PetServiceCategoryId,
                BasePrice = addPetService.BasePrice,
                Duration = addPetService.Duration,
                ImageUrl = addPetService.ImageUrl,
                AvailableFrom = addPetService.AvailableFrom,
                AvailableTo = addPetService.AvailableTo,
                TravelCost = addPetService.TravelCost,
                Description = addPetService.Description,
                MaxNumberOfPets = addPetService.MaxNumberOfPets,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
            };

            // Create the service
            var createResult = await _unitOfWork.PetServiceRepository.CreateServiceAsync(petService);
            if (createResult == null)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceNotCreated());
            }
            var response = new CreateResponse { Id = petService.Id };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetAllPetServicesAsync()
        {
            var petServices = await _unitOfWork.PetServiceRepository.GetAllServicesAsync();
            return Result.SuccessWithObject(petServices);
        }

        public async Task<Result> GetAllPetServicesAsync(string searchName, string searchDuration, string searchCategoryName, int pageIndex, int pageSize)
        {
            var petServices = await _unitOfWork.PetServiceRepository.GetAllServiceWithSearchAsync(searchName, searchDuration, searchCategoryName, pageIndex, pageSize);

            var pagedResult = new PagedResultSearch<PetService>
            {
                Items = petServices.petServices,
                TotalItems = petServices.totalItems,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(pagedResult);
        }

        public async Task<Result> GetPetServiceByIdAsync(Guid id)
        {
            var petService = await _unitOfWork.PetServiceRepository.GetServiceByIdAsync(id);
            if (petService == null)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceNotFound());
            }

            return Result.SuccessWithObject(petService);
        }

        public async Task<Result> UpdatePetServiceAsync(Guid id, AddPetServiceRequest addPetService)
        {
            // Validate the input
            var validationResult = await _petServiceValidator.ValidateAsync(addPetService);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                var result= Result.Failures(errors);
                return result;
            }
            var existingPetService = await _unitOfWork.PetServiceRepository.GetServiceByIdAsync(id);
            if (existingPetService == null)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceNotFound());
            }
            var categoryExists = await _unitOfWork.PetServiceCategoryRepository.GetByIdAsync(addPetService.PetServiceCategoryId);
            if (categoryExists == null)
            {
                return Result.Failure(PetServiceErrorMessage.InvalidFieldValue("PetServiceCategory"));
            }
            // Update the properties
            existingPetService.Name = addPetService.Name; // Update Name
            existingPetService.PetServiceCategoryId = addPetService.PetServiceCategoryId;
            existingPetService.BasePrice = addPetService.BasePrice;
            existingPetService.Duration = addPetService.Duration;
            existingPetService.ImageUrl = addPetService.ImageUrl;
            existingPetService.AvailableFrom = addPetService.AvailableFrom;
            existingPetService.AvailableTo = addPetService.AvailableTo;
            existingPetService.TravelCost = addPetService.TravelCost;
            existingPetService.Description = addPetService.Description; 
            existingPetService.MaxNumberOfPets = addPetService.MaxNumberOfPets;
            existingPetService.ModifiedDate = DateTime.UtcNow;

            // Update the service
            var updateResult = await _unitOfWork.PetServiceRepository.UpdateAsync(existingPetService);

            if (updateResult == 0)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceUpdateFailed());
            }

            var response = new CreateResponse { Id = existingPetService.Id };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> DeletePetServiceAsync(Guid id)
        {
            var existingPetService = await _unitOfWork.PetServiceRepository.GetServiceByIdAsync(id);
            if (existingPetService == null)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceNotFound());
            }
            var deleteResult = await _unitOfWork.PetServiceRepository.DeleteServiceAsync(id);
            if (deleteResult == 0)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceDeleteFailed());
            }

            return Result.SuccessWithObject(deleteResult);
        }
        public async Task<List<PetService>> GetServicesExpiringSoonAsync()
        {
            var result = await _unitOfWork.PetServiceRepository.GetServicesExpiringSoonAsync();
            return result;
        }
        public async Task<Result> UpdatePetServiceAsync(PetService service)
        {
            var result = await _unitOfWork.PetServiceRepository.UpdateAsync(service);
            if (result == 0)
            {
                return Result.Failure(PetServiceErrorMessage.PetServiceUpdateFailed());
            }

            var response = new CreateResponse { Id = service.Id };
            return Result.SuccessWithObject(response);
        }
        public async Task<List<ServiceFrequency>> GetTopServicesAsync()
        {
            var result = await _unitOfWork.PetServiceRepository.GetTopServicesAsync();
            return result;
        }
    }
}
