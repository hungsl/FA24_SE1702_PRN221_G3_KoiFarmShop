using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Infrastructure.Interface;

namespace KoiFarmShop.Application.Implement.Service
{
    public class PetServiceLogic : IPetServiceLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddPetRequest> _addPetRequest;


        public PetServiceLogic(IUnitOfWork unitOfWork,
            IValidator<AddPetRequest> addPetRequest)
        {
            _unitOfWork = unitOfWork;
            _addPetRequest = addPetRequest;
        }


        // Retrieve pets by owner ID with error handling
        public async Task<Result> GetPetsByOwnerIdAsync(Guid ownerId)
        {
            var pets = await _unitOfWork.PetRepository.GetPetsByOwnerIdAsync(ownerId);

            if (pets == null || !pets.Any())
            {
                // If no pets found, return a not found error
                return Result.Failure(PetErrorMessage.PetNotFound());
            }

            // Return the list of pets if found
            return Result.SuccessWithObject(pets);
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
            var validate = await _addPetRequest.ValidateAsync(addPet);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();

                return Result.Failures(errors);
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
                Quantity = addPet.Quantity,
                LastHealthCheck = addPet.LastHealthCheck,
                Note = addPet.Note,
                HealthStatus = addPet.HealthStatus
            };

            var createResult = await _unitOfWork.PetRepository.CreatePetAsync(pet);
            if (createResult == null)
            {
                return Result.Failure(PetErrorMessage.PetCreateFailed());
            }

            var response = new AddPetResponse { Id = pet.Id };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> UpdatePetAsync(Guid id, AddPetRequest updatePet)
        {
            var validate = await _addPetRequest.ValidateAsync(updatePet);
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
            pet.Quantity = updatePet.Quantity;
            pet.LastHealthCheck = updatePet.LastHealthCheck;
            pet.Note = updatePet.Note;
            pet.HealthStatus = updatePet.HealthStatus;

            var updateResult = await _unitOfWork.PetRepository.UpdatePetAsync(pet);
            if (updateResult == 0)
            {
                return Result.Failure(PetErrorMessage.PetUpdateFailed());
            }

            var response = new AddPetResponse { Id = pet.Id };
            return Result.SuccessWithObject(response);
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

        public async Task<Result> GetSearchPetAsync(string searchName, string searchColor, string searchNote)
        {
            var pet = await _unitOfWork.PetRepository.GetAllPetWithSearchAsync(searchName, searchColor, searchNote);

            var pagedResult = new ResultSearch<Pet>
            {
                Items = pet
            };

            return Result.SuccessWithObject(pagedResult);
        }
    }
}
