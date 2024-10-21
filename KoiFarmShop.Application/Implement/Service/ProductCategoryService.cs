using FluentValidation;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.Interface;
using KVSC.Application.Interface.IService;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Common.Message;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using KVSC.Infrastructure.DTOs.Product.GetProduct;
using KVSC.Infrastructure.DTOs.Product.UpdateProduct;
using KVSC.Infrastructure.DTOs.ProductCategory.AddProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.DeleteProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.GetProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.UpdateProductCategory;
using KVSC.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Implement.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddProductCategoryRequest> _addProductCategoryRequestValidator;
        private readonly IValidator<UpdateProductCategoryRequest> _updateProductCategoryRequestValidator;

        public ProductCategoryService(
            IUnitOfWork unitOfWork,
            IValidator<AddProductCategoryRequest> addProductCategoryRequestValidator,
            IValidator<UpdateProductCategoryRequest> updateProductCategoryRequestValidator
        )
        {
            _unitOfWork = unitOfWork;
            _addProductCategoryRequestValidator = addProductCategoryRequestValidator;
            _updateProductCategoryRequestValidator = updateProductCategoryRequestValidator;
        }
        public async Task<Result> AddProductCategoryAsync(AddProductCategoryRequest addProductCategoryRequest)
        {
            // Validate the AddProductRequest
            var validate = await _addProductCategoryRequestValidator.ValidateAsync(addProductCategoryRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            // Create a new product entity
            var productCategory = new ProductCategory
            {
                Name = addProductCategoryRequest.Name,
                Description = addProductCategoryRequest.Description,
            };

            // Add the product to the database
            var createResult = await _unitOfWork.ProductCategoryRepository.CreateProductCategoryAsync(productCategory);
            if (createResult == 0) // Adjust according to your repository's CreateAsync method return type
            {
                return Result.Failure(ProductCategoryErrorMessage.CategoryCreatedFailed());
            }
            var result = new AddProductCategoryResponse
            {
                ProductCategoryId = productCategory.Id,
                CreatedDate = productCategory.CreatedDate,
                Name = productCategory.Name,
                Description = productCategory.Description

            };
            return Result.SuccessWithObject(result);
        }

        // Read (Retrieve a product by ID)
        public async Task<Result> GetProductCategoryByIdAsync(GetProductCategoryRequest getProductCategoryRequest)
        {
            var productCategory = await _unitOfWork.ProductCategoryRepository.GetProductCategoryByIdAsync(getProductCategoryRequest.ProductCategoryId);
            if (productCategory == null)
            {
                return Result.Failure(ProductCategoryErrorMessage.CategoryNotExist());
            } 

            var productResponse = new GetProductCategoryResponse
            {
                ProductCategoryId = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                CreatedDate = productCategory.CreatedDate,
                ModifiedDate = productCategory.ModifiedDate,
                IsDeleted = productCategory.IsDeleted,
            };
            return Result.SuccessWithObject(productResponse);
        }

        public async Task<Result> UpdateProductCategoryAsync(UpdateProductCategoryRequest updateProductCategoryRequest)
        {
            var validate = await _updateProductCategoryRequestValidator.ValidateAsync(updateProductCategoryRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors.Select(e => (Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var product = await _unitOfWork.ProductCategoryRepository.GetProductCategoryByIdAsync(updateProductCategoryRequest.ProductCategoryId);
            if (product == null)
            {
                return Result.Failure(ProductCategoryErrorMessage.CategoryNotExist());
            }

            // Update fields
            if (updateProductCategoryRequest.Name != null)
                product.Name = updateProductCategoryRequest.Name;

            if (updateProductCategoryRequest.Description != null)
                product.Description = updateProductCategoryRequest.Description;

            var updateResult = await _unitOfWork.ProductCategoryRepository.UpdateProductCategoryAsync(product);
            if (updateResult == 0)
            {
                return Result.Failure(ProductCategoryErrorMessage.CategoryUpdateFailed());
            }
            var productCategoryAfter = await _unitOfWork.ProductCategoryRepository.GetProductCategoryByIdAsync(updateProductCategoryRequest.ProductCategoryId);
            var response = new UpdateProductCategoryResponse
            {
                ProductCategoryId = productCategoryAfter.Id,
                Name = productCategoryAfter.Name,
                Description = productCategoryAfter.Description
            };
            return Result.SuccessWithObject(response);
        }

        // Delete
        public async Task<Result> DeleteProductCategoryAsync(DeleteProductCategoryRequest request)
        {
            
            var product = await _unitOfWork.ProductCategoryRepository.GetProductCategoryByIdAsync(request.ProductCategoryId);
            if (product == null)
            {
                return Result.Failure(ProductCategoryErrorMessage.CategoryNotFound());
            }

            var deleteResult = await _unitOfWork.ProductCategoryRepository.RemoveProductCategoryByIdAsync(request.ProductCategoryId);
            if (!deleteResult)
            {
                return Result.Failure(ProductCategoryErrorMessage.CategoryDeletionFailed());
            }

            var response = new DeleteProductCategoryResponse
            {
                ProductCategoryId = product.Id,
                Name = product.Name // Add the category name to the response
            };

            return Result.SuccessWithObject(response);
        }

    }
}
