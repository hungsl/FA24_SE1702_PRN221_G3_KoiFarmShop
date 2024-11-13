using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.Interface;
using KVSC.Application.Common.Validator.Abstract;
using KVSC.Application.Interface.IService;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Common.Message;
using KVSC.Infrastructure.DTOs.Paging;
using KVSC.Infrastructure.DTOs.Rating.AddRating;
using KVSC.Infrastructure.DTOs.Rating.GetRating;
using KVSC.Infrastructure.DTOs.Rating.UpdateRating;
using KVSC.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Implement.Service
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddRatingRequest> _addRatingValidator;
        private readonly IValidator<UpdateRatingRequest> _updateRatingValidator;
        public RatingService(IUnitOfWork unitOfWork, IValidator<AddRatingRequest> addRatingValidator, IValidator<UpdateRatingRequest> updateRatingValidator)
        {
            _unitOfWork = unitOfWork;
            _addRatingValidator = addRatingValidator;
            _updateRatingValidator = updateRatingValidator;
        }

        public async Task<Result> CreateRatingAsync(AddRatingRequest addRatingRequest)
        {
            var validationResult = await _addRatingValidator.ValidateAsync(addRatingRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            // Tạo đối tượng Rating
            var rating = new Rating
            {
                ServiceId = addRatingRequest.ServiceId,
                CustomerId = addRatingRequest.CustomerId,
                Score = addRatingRequest.Score,
                Feedback = addRatingRequest.Feedback,
                ModifiedDate = null,
                IsDeleted = false
            };

            // Lưu vào database
            var createResult = await _unitOfWork.RatingRepository.CreateRatingAsync(rating);
            if (createResult == null)
            {
                return Result.Failure(RatingErrorMessage.RatingNotCreated());
            }

            var response = new CreateResponse { Id = createResult.Id };
            return Result.SuccessWithObject(response);
        }
        public async Task<Result> GetRatingsByServiceIdAsync(Guid serviceId)
        {
            var ratings = await _unitOfWork.RatingRepository.GetByServiceIdAsync(serviceId);
            if (ratings == null || !ratings.Any())
            {
                return Result.Failure(RatingErrorMessage.RatingNotFound());
            }
            var ratingResponses = new List<GetRatingResponse>();
            foreach (var r in ratings)
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(r.CustomerId);
                var service = await _unitOfWork.PetServiceRepository.GetServiceByIdAsync(r.ServiceId);
                ratingResponses.Add(new GetRatingResponse
                {
                    Id = r.Id,
                    ServiceId = r.ServiceId,
                    CustomerId = r.CustomerId,
                    CustomerName = user.FullName ?? string.Empty,
                    ServiceName = service.Name ?? string.Empty,
                    Score = r.Score,
                    Feedback = r.Feedback,
                    CreatedDate = r.CreatedDate
                });
            }
            return Result.SuccessWithObject(ratingResponses);
        }
        public async Task<Result> GetAllRatingsAsync(GetAllRatingsRequest request)
        {

            var ratings = await _unitOfWork.RatingRepository.GetAll(request);
            var ratingResponses = new List<GetRatingResponse>();
            foreach (var r in ratings.Ratings)
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(r.CustomerId);
                var service = await _unitOfWork.PetServiceRepository.GetServiceByIdAsync(r.ServiceId);
                ratingResponses.Add(new GetRatingResponse
                {
                    Id = r.Id,
                    ServiceId = r.ServiceId,
                    CustomerId = r.CustomerId,
                    CustomerName = user.FullName ?? string.Empty,
                    ServiceName = service.Name ?? string.Empty,
                    Score = r.Score,
                    Feedback = r.Feedback,
                    CreatedDate = r.CreatedDate
                });
            }

            var pagedResponse = new PagedResponse<GetRatingResponse>
            {
                Data = ratingResponses,
                TotalCount = ratings.TotalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return Result.SuccessWithObject(pagedResponse);
        }
        public async Task<Result> UpdateRatingAsync(UpdateRatingRequest updateRatingRequest)
        {
            var validationResult = await _updateRatingValidator.ValidateAsync(updateRatingRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => (Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var existingRating = await _unitOfWork.RatingRepository.GetByIdAsync(updateRatingRequest.Id);
            if (existingRating == null)
            {
                return Result.Failure(RatingErrorMessage.RatingNotFound());
            }

            // Update properties
            existingRating.Score = updateRatingRequest.Score;
            existingRating.Feedback = updateRatingRequest.Feedback;
            existingRating.ModifiedDate = DateTime.UtcNow;

            // Update in the database
            var updateResult = await _unitOfWork.RatingRepository.UpdateAsync(existingRating);
            if (updateResult == 0)
            {
                return Result.Failure(RatingErrorMessage.RatingUpdateFailed());
            }

            var response = new CreateResponse { Id = existingRating.Id };
            return Result.SuccessWithObject(response);
        }
        public async Task<Result> DeleteRatingAsync(Guid id)
        {
            var existingRating = await _unitOfWork.RatingRepository.GetByIdAsync(id);
            if (existingRating == null)
            {
                return Result.Failure(RatingErrorMessage.RatingNotFound());
            }

            var deleteResult = await _unitOfWork.RatingRepository.DeleteRatingAsync(id);
            if (deleteResult == 0)
            {
                return Result.Failure(RatingErrorMessage.RatingDeleteFailed());
            }

            return Result.SuccessWithObject(deleteResult);
        }
    }
}
