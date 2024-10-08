using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPetHabitat;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePetHabitat;
using KoiFarmShop.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Implement.Service
{
    public class PetHabitatService : IPetHabitatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddPetHabitatRequest> _addPetHabitatValidator;
        private readonly IValidator<UpdatePetHabitatRequest> _updatePetHabitatValidator;

        public PetHabitatService(IUnitOfWork unitOfWork,
            IValidator<AddPetHabitatRequest> addPetHabitatValidator,
            IValidator<UpdatePetHabitatRequest> updatePetHabitatValidator)
        {
            _unitOfWork = unitOfWork;
            _addPetHabitatValidator = addPetHabitatValidator;
            _updatePetHabitatValidator = updatePetHabitatValidator;
        }

        public async Task<Result> GetPetHabitatByIdAsync(Guid id)
        {
            var petHabitat = await _unitOfWork.PetHabitatRepository.GetPetHabitatByIdAsync(id);
            if (petHabitat == null)
            {
                return Result.Failure(PetErrorMessage.PetHabitatNotFound());
            }

            return Result.SuccessWithObject(petHabitat);
        }

        public async Task<Result> GetAllPetHabitatsAsync()
        {
            var petHabitats = await _unitOfWork.PetHabitatRepository.GetAllPetHabitatsAsync();
            return Result.SuccessWithObject(petHabitats);
        }

        public async Task<Result> CreatePetHabitatAsync(AddPetHabitatRequest addPetHabitatRequest)
        {
            var validate = await _addPetHabitatValidator.ValidateAsync(addPetHabitatRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();

                return Result.Failures(errors);
            }

            var petHabitat = new PetHabitat
            {
                Id = Guid.NewGuid(),
                HabitatType = addPetHabitatRequest.HabitatType.ToLower()
            };

            var createResult = await _unitOfWork.PetHabitatRepository.CreatePetHabitatAsync(petHabitat);
            if (createResult == null)
            {
                return Result.Failure(PetErrorMessage.PetHabitatCreateFailed());
            }
            return Result.SuccessWithObject(createResult.Id);
        }

        public async Task<Result> UpdatePetHabitatAsync(Guid id, UpdatePetHabitatRequest updatePetHabitatRequest)
        {
            var validate = await _updatePetHabitatValidator.ValidateAsync(updatePetHabitatRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();

                return Result.Failures(errors);
            }

            var petHabitat = await _unitOfWork.PetHabitatRepository.GetPetHabitatByIdAsync(id);
            if (petHabitat == null)
            {
                return Result.Failure(PetErrorMessage.PetHabitatNotFound());
            }

            petHabitat.HabitatType = updatePetHabitatRequest.HabitatType.ToLower();

            var updateResult = await _unitOfWork.PetHabitatRepository.UpdatePetHabitatAsync(petHabitat);
            if (updateResult == 0)
            {
                return Result.Failure(PetErrorMessage.PetHabitatUpdateFailed());
            }
            return Result.SuccessWithObject(updateResult);
        }

        public async Task<Result> DeletePetHabitatAsync(Guid id)
        {
            var petHabitat = await _unitOfWork.PetHabitatRepository.GetPetHabitatByIdAsync(id);
            if (petHabitat == null)
            {
                return Result.Failure(PetErrorMessage.PetHabitatNotFound());
            }

            var deleteResult = await _unitOfWork.PetHabitatRepository.DeletePetHabitatAsync(id);
            if (deleteResult == 0)
            {
                return Result.Failure(PetErrorMessage.PetHabitatDeleteFailed());
            }
            return Result.SuccessWithObject(deleteResult);
        }
    }
}
