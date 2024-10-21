using FluentValidation;
using KVSC.Application.Interface.IService;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.Interface;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using System;
using System.Linq;
using System.Threading.Tasks;
using KVSC.Infrastructure.DTOs.Common.Message;
using KVSC.Infrastructure.DTOs.Product.UpdateProduct;
using KVSC.Infrastructure.DTOs.Product.SearchProduct;
using KVSC.Infrastructure.DTOs.Product.GetProduct;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KoiFarmShop.Application.Common.Result;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.DTOs.Firebase.AddImage;
using KoiFarmShop.Infrastructure.DTOs.Firebase.GetImage;

namespace KVSC.Application.Implement.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddProductRequest> _addProductRequestValidator;
        private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator;

        public ProductService(
            IUnitOfWork unitOfWork,
            IValidator<AddProductRequest> addProductRequestValidator,
            IValidator<UpdateProductRequest> updateProductRequestValidator
        )
        {
            _unitOfWork = unitOfWork;
            _addProductRequestValidator = addProductRequestValidator;
            _updateProductRequestValidator = updateProductRequestValidator;
            
        }

        public async Task<Result> AddProductAsync(AddProductRequest addProductRequest)
        {
            // Validate the AddProductRequest
            var validate = await _addProductRequestValidator.ValidateAsync(addProductRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            // Check if product name already exists
            var productExists = await _unitOfWork.ProductRepository.ProductNameExistsAsync(addProductRequest.Name);
            if (productExists)
            {
                return Result.Failure(ProductErrorMessage.ProductNameIsExist());
            }

            // Upload the image and get the result
            AddImageRequest imageRequest = new AddImageRequest(addProductRequest.ImageFile, "Products");

            var uploadImageResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest); // Assuming 'Image' is the property in AddProductRequest for the image file

            if (!uploadImageResult.Success)
            {
                return Result.Failure(uploadImageResult.Error); // Return the error from image upload
            }

            // Create a new product entity
            var product = new Product
            {
                Name = addProductRequest.Name,
                Price = addProductRequest.Price,
                Description = addProductRequest.Description,
                ProductCategoryId = addProductRequest.ProductCategoryId, // Assuming this is in the request
                StockQuantity = addProductRequest.StockQuantity, // Assuming this is in the request, set to 0 if no stock data is provided
                ImageUrl = uploadImageResult.FilePath // Optional, but set if available
            };

            // Add the product to the database
            var createResult = await _unitOfWork.ProductRepository.CreateProductAsync(product);
            if (createResult == 0) // Adjust according to your repository's CreateAsync method return type
            {
                return Result.Failure(ProductErrorMessage.ProductNameIsExist());
            }

            return Result.SuccessWithObject(createResult);
        }
        // Read (Retrieve a product by ID)
        public async Task<Result> GetProductByIdAsync(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotExist());
            }
            // Retrieve the product image from Firebase (assuming FirebaseRepository has a method to get the image)
            GetImageRequest getImageRequest = new GetImageRequest(product.ImageUrl);
            var imageResult = await _unitOfWork.FirebaseRepository.GetImageAsync(getImageRequest); // Assuming product.ImageUrl stores the image reference
            if (imageResult == null)
            {
                return Result.Failure(imageResult.Error); // Handle image retrieval failure
            }

            var productResponse = new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ProductCategoryId = product.ProductCategoryId,
                StockQuantity = product.StockQuantity,
                ImageStream = imageResult.ImageUrl,   // Store image data in MemoryStream
                //ProductCategoryName = product.ProductCategory?.Name // Optional: Category name, if available
            };
            return Result.SuccessWithObject(productResponse);
        }
        public async Task<Result> GetProductsAsync(SearchProductRequest request)
        {
            var response = await _unitOfWork.ProductRepository.GetProductsAsync(request);
            if (response.Error != Error.None) // Assuming Error.None means no error
            {
                return Result.Failure(response.Error);
            }
            return Result.SuccessWithObject(response);
        }

        // Update
        public async Task<Result> UpdateProductAsync(UpdateProductRequest updateProductRequest)
        {
            var validate = await _updateProductRequestValidator.ValidateAsync(updateProductRequest);
            if (!validate.IsValid)
            {
                var errors = validate.Errors.Select(e => (Error)e.CustomState).ToList();
                return Result.Failures(errors);
            }

            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(updateProductRequest.Id);
            if (product == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotExist());
            }

            // Update fields
            if (updateProductRequest.Name != null)
                product.Name = updateProductRequest.Name;

            if (updateProductRequest.Description != null)
                product.Description = updateProductRequest.Description;

            if (updateProductRequest.Price.HasValue)
                product.Price = updateProductRequest.Price.Value;

            if (updateProductRequest.StockQuantity.HasValue)
                product.StockQuantity = updateProductRequest.StockQuantity.Value;

            if (updateProductRequest.ImageUrl != null)
                product.ImageUrl = updateProductRequest.ImageUrl;

            var updateResult = await _unitOfWork.ProductRepository.UpdateProductAsync(product);
            return updateResult == 0
                ? Result.Failure(ProductErrorMessage.ProductUpdateFailed())
                : Result.Success();
        }

        // Delete
        public async Task<Result> DeleteProductAsync(Guid productId)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotFound());
            }

            var deleteResult = await _unitOfWork.ProductRepository.RemoveProductAsync(product);
            return deleteResult ? Result.Success() : Result.Failure(ProductErrorMessage.ProductDeletionFailed());
        }

        Task<Result> IProductService.AddProductAsync(AddProductRequest addProductRequest)
        {
            throw new NotImplementedException();
        }

        Task<Result> IProductService.UpdateProductAsync(UpdateProductRequest updateProductRequest)
        {
            throw new NotImplementedException();
        }

        Task<Result> IProductService.DeleteProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        Task<Result> IProductService.GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Result> IProductService.GetProductsAsync(SearchProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
