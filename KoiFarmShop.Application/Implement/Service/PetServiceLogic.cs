using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePet;
using KoiFarmShop.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Implement.Service
{
    public class PetServiceLogic : IPetServiceLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddPetRequest> _addPetRequestValidator;
        private readonly IValidator<UpdatePetRequest> _updatePetRequestValidator;


        public PetServiceLogic(IUnitOfWork unitOfWork,
            IValidator<AddPetRequest> addPetRequestValidator,
            IValidator<UpdatePetRequest> updatePetRequestValidator)
        {
            _unitOfWork = unitOfWork;
            _addPetRequestValidator = addPetRequestValidator;
            _updatePetRequestValidator = updatePetRequestValidator;
        }

        public async Task<Result> GetPetByIdAsync(Guid id)
        {
            var pet = await _unitOfWork.PetRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                return Result.Failure(PetErrorMessage.PetNotFound());
            }

            return Result.SuccessWithObject(pet);
        }

        public async Task<Result> GetAllPetAsync()
        {
            var pets = await _unitOfWork.PetRepository.GetAllPetAsync();
            return Result.SuccessWithObject(pets);
        }

        public async Task<Result> CreatePetAsync(AddPetRequest addPet)
        {
            var validate = await _addPetRequestValidator.ValidateAsync(addPet);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();

                return Result.Failures(errors);
            }

            //check if pet has user 
            var owner = await _unitOfWork.UserRepository.GetByIdAsync(addPet.OwnerId);
            if (owner == null)
            {
                return Result.Failure(UserErrorMessage.UserNotExist());
            }

            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = addPet.Name,
                Age = addPet.Age,
                Gender = addPet.Gender,
                ImageUrl = addPet.ImageUrl,
                Color = addPet.Color,
                Length = addPet.Length,
                Weight = addPet.Weight,
                LastHealthCheck = addPet.LastHealthCheck,
                HealthStatus = addPet.HealthStatus,
                OwnerId = addPet.OwnerId,
                PetTypeId = addPet.PetTypeId
            };

            var createResult = await _unitOfWork.PetRepository.CreatePetAsync(pet);
            if (createResult == null)
            {
                return Result.Failure(PetErrorMessage.PetCreateFailed());
            }

            return Result.SuccessWithObject(createResult.Id);
        }

        public async Task<Result> UpdatePetAsync(Guid id, UpdatePetRequest updatePet)
        {
            var validate = await _updatePetRequestValidator.ValidateAsync(updatePet);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();

                return Result.Failures(errors);
            }

            var pet = await _unitOfWork.PetRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                return Result.Failure(PetErrorMessage.PetNotFound());
            }

            pet.Name = updatePet.Name;
            pet.Age = updatePet.Age;
            pet.Gender = updatePet.Gender;
            pet.ImageUrl = updatePet.ImageUrl;
            pet.Color = updatePet.Color;
            pet.Length = updatePet.Length;
            pet.Weight = updatePet.Weight;
            pet.LastHealthCheck = updatePet.LastHealthCheck;
            pet.HealthStatus = updatePet.HealthStatus;
            pet.PetTypeId = updatePet.PetTypeId;

            var updateResult = await _unitOfWork.PetRepository.UpdatePetAsync(pet);
            if (updateResult == 0)
            {
                return Result.Failure(PetErrorMessage.PetUpdateFailed());
            }

            return Result.SuccessWithObject(updateResult);
        }

        public async Task<Result> DeletePetAsync(Guid id)
        {
            var pet = await _unitOfWork.PetRepository.GetPetByIdAsync(id);
            if (pet == null)
            {
                return Result.Failure(PetErrorMessage.PetNotFound());
            }

            var deleteResult = await _unitOfWork.PetRepository.DeletePetAsync(id);
            if (deleteResult == 0)
            {
                return Result.Failure(PetErrorMessage.PetDeleteFailed());
            }

            return Result.SuccessWithObject(deleteResult);
        }
    }
}
