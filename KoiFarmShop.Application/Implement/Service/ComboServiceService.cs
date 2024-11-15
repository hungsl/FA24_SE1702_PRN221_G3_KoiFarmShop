using FluentValidation;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;

namespace KoiFarmShop.Application.Implement.Service
{
    public class ComboServiceService : IComboServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddComboServiceRequest> _comboServiceValidator;
        public ComboServiceService(IUnitOfWork unitOfWork, IValidator<AddComboServiceRequest> comboServiceValidator)
        {
            _unitOfWork = unitOfWork;
            _comboServiceValidator = comboServiceValidator;
        }
        public async Task<Result> CreateComboServiceAsync(AddComboServiceRequest addComboService)
        {
            var validationResult = await _comboServiceValidator.ValidateAsync(addComboService);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                       .Select(e => (Error)e.CustomState)
                       .ToList();
                return Result.Failures(errors);
            }
            // Remove duplicate serviceIds and list 
            var distinctServiceIds = addComboService.ServiceIds.Distinct().ToList();

            // Check if there are at least 2 distinct services
            if (distinctServiceIds.Count < 2)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceInsufficientServices());
            }

            // Fetch existing services from database
            var existingServices = await _unitOfWork.PetServiceRepository
                                  .GetByIdsAsync(distinctServiceIds);

            // Check if all serviceIds exist in the database
            if (existingServices.Count != distinctServiceIds.Count)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceInvalidServiceIds());
            }

            var comboService = new ComboService
            {
                Name = addComboService.Name,
                DiscountPercentage = addComboService.DiscountPercentage,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                ComboServiceItems = addComboService.ServiceIds.Select(serviceId => new ComboServiceItem
                {
                    PetServiceId = serviceId
                }).ToList()
            };
            var createResult = await _unitOfWork.ComboServiceRepository.CreateAsync(comboService);
            if (createResult == null)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceNotCreated());
            }
            var response = new CreateResponse { Id = comboService.Id };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetComboServiceByIdAsync(Guid id)
        {
            var comboService = await _unitOfWork.ComboServiceRepository.GetComboByIdAsync(id);

            if (comboService == null || comboService.IsDeleted)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceNotFound());
            }

            var comboServiceResponse = new ComboServiceResponse
            {
                Id = comboService.Id,
                Name = comboService.Name,
                DiscountPercentage = comboService.DiscountPercentage,
                ServiceIds = comboService.ComboServiceItems
                                         .Select(csi => csi.PetServiceId)
                                         .ToList(),
                TotalPrice = comboService.TotalPrice
            };

            return Result.SuccessWithObject(comboServiceResponse);
        }
        public async Task<Result> GetAllComboServicesAsync()
        {
            var comboServices = await _unitOfWork.ComboServiceRepository.GetAllComboAsync();

            var comboServiceResponses = comboServices
                .Where(cs => !cs.IsDeleted) // Lọc bỏ các combo đã bị xóa
                .Select(cs => new ComboServiceResponse
                {
                    Id = cs.Id,
                    Name = cs.Name,
                    DiscountPercentage = cs.DiscountPercentage,
                    ServiceIds = cs.ComboServiceItems
                                    .Select(csi => csi.PetServiceId)
                                    .ToList(),
                    TotalPrice = cs.TotalPrice
                })
                .ToList();

            return Result.SuccessWithObject(comboServiceResponses);
        }

        public async Task<Result> UpdateComboServiceAsync(Guid id, AddComboServiceRequest addComboService)
        {
            var comboService = await _unitOfWork.ComboServiceRepository.GetComboByIdAsync(id);
            if (comboService == null || comboService.IsDeleted)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceNotFound());
            }
            // Kiểm tra các serviceId có tồn tại không
            var distinctServiceIds = addComboService.ServiceIds.Distinct().ToList();
            if (distinctServiceIds.Count < 2)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceInsufficientServices());
            }

            // Kiểm tra xem các serviceId có tồn tại không
            var existingServices = await _unitOfWork.PetServiceRepository.GetByIdsAsync(distinctServiceIds);
            if (existingServices.Count != distinctServiceIds.Count)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceInvalidServiceIds());
            }

            comboService.Name = addComboService.Name;
            comboService.DiscountPercentage = addComboService.DiscountPercentage;
            comboService.ModifiedDate = DateTime.UtcNow;
            // Xóa các ComboServiceItem không còn nằm trong danh sách distinctServiceIds
            var itemsToRemove = comboService.ComboServiceItems
                .Where(item => !distinctServiceIds.Contains(item.PetServiceId))
                .ToList();
            foreach (var item in itemsToRemove)
            {
                comboService.ComboServiceItems.Remove(item);
            }
            // Thêm các ComboServiceItem mới từ distinctServiceIds
            foreach (var serviceId in distinctServiceIds)
            {
                // Kiểm tra xem đã tồn tại chưa
                if (!comboService.ComboServiceItems.Any(item => item.PetServiceId == serviceId))
                {
                    comboService.ComboServiceItems.Add(new ComboServiceItem
                    {
                        PetServiceId = serviceId
                    });
                }
            }

            var updateResult = await _unitOfWork.ComboServiceRepository.UpdateAsync(comboService);
            if (updateResult == 0)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceUpdateFailed());
            }

            var response = new CreateResponse { Id = comboService.Id };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> DeleteComboServiceAsync(Guid id)
        {
            var comboService = await _unitOfWork.ComboServiceRepository.GetByIdAsync(id);
            if (comboService == null || comboService.IsDeleted)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceNotFound());
            }

            comboService.IsDeleted = true;
            var deleteResult = await _unitOfWork.ComboServiceRepository.UpdateAsync(comboService);
            if (deleteResult == 0)
            {
                return Result.Failure(ComboServiceErrorMessage.ComboServiceDeleteFailed());
            }

            return Result.SuccessWithObject(deleteResult);
        }
    }
}
