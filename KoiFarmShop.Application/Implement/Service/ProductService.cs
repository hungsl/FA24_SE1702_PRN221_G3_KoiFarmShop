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
using KVSC.Application.Common.Validator.Abstract;
using Microsoft.AspNetCore.Http;
using KoiFarmShop.Application.Interface.IService;

namespace KVSC.Application.Implement.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<AddProductRequest> _addProductRequestValidator;
        private readonly IValidator<UpdateProductRequest> _updateProductRequestValidator;
        private readonly IFirebaseService _firebaseService;

        public ProductService(
            IUnitOfWork unitOfWork,
            IValidator<AddProductRequest> addProductRequestValidator,
            IValidator<UpdateProductRequest> updateProductRequestValidator,
            IFirebaseService firebaseService
        )
        {
            _unitOfWork = unitOfWork;
            _addProductRequestValidator = addProductRequestValidator;
            _updateProductRequestValidator = updateProductRequestValidator;
            _firebaseService = firebaseService;
        }

        // Create Product
        public async Task<Result> CreateProductAsync(AddProductRequest addProductRequest)
        {
            // Validate input
            var validationResult = await _addProductRequestValidator.ValidateAsync(addProductRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            //// Handle image upload
            //AddImageRequest imageRequest = new AddImageRequest(addProductRequest.ImageFile, "Products");

            //var uploadImageResult = await _unitOfWork.FirebaseRepository.UploadImageAsync(imageRequest); // Assuming 'Image' is the property in AddProductRequest for the image file

            //if (!uploadImageResult.Success)
            //{
            //    return Result.Failure(uploadImageResult.Error); // Return the error from image upload
            //}

            var product = new Product
            {
                Name = addProductRequest.Name,
                Description = addProductRequest.Description,
                Price = addProductRequest.Price,
                StockQuantity = addProductRequest.StockQuantity,
                ImageUrl = "Image",
                ProductCategoryId = addProductRequest.ProductCategoryId,
                DiscountPrice = addProductRequest.DiscountPrice,
                ReleaseDate = addProductRequest.ReleaseDate,
                SKU = addProductRequest.SKU,
                Manufacturer = addProductRequest.Manufacturer,
                ProductDimensions = addProductRequest.ProductDimensions,
                Weight = addProductRequest.Weight,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var createResult = await _unitOfWork.ProductRepository.CreateProductAsync(product);
            if (createResult == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotCreated());
            }
            return Result.SuccessWithObject(new { Id = product.Id });
        }

        // Get All Products
        public async Task<Result> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllProductsAsync();
            return Result.SuccessWithObject(products);
        }

        // Get Products with Search
        public async Task<Result> GetAllProductsAsync(string searchTerm, string searchField, int pageIndex, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, decimal? minDiscountPrice = null, decimal? maxDiscountPrice = null)
        {
            var result = await _unitOfWork.ProductRepository.GetAllProductsWithSearchAsync(searchTerm, searchField, pageIndex, pageSize, minPrice, maxPrice, minDiscountPrice, maxDiscountPrice);

            var pagedResult = new PagedResultSearch<Product>
            {
                Items = result.products,
                TotalItems = result.totalItems,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(pagedResult);
        }

        // Get Product By Id
        public async Task<Result> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotFound());
            }

            return Result.SuccessWithObject(product);
        }

        // Update Product
        public async Task<Result> UpdateProductAsync(Guid id, UpdateProductRequest updateProductRequest)
        {
            // Validate input
            var validationResult = await _updateProductRequestValidator.ValidateAsync(updateProductRequest);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => (Error)e.CustomState)
                    .ToList();
                return Result.Failures(errors);
            }

            var existingProduct = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotFound());
            }

            // Handle image file update (if a new image is uploaded)
            //if (updateProductRequest.ImageFile != null)
            //{
            //    // Assuming you have a service to upload files
            //    var uploadResult = await _firebaseService.UploadImageAsync(updateProductRequest.ImageFile, "products");
            //    if (!uploadResult.IsSuccess)
            //    {
            //        return Result.Failure(uploadResult.Error);
            //    }
            //    existingProduct.ImageUrl = uploadResult.Object.ToString(); // Update the image URL
            //}

            // Update other fields
            existingProduct.Name = updateProductRequest.Name ?? existingProduct.Name;
            existingProduct.Description = updateProductRequest.Description ?? existingProduct.Description;
            existingProduct.Price = updateProductRequest.Price ?? existingProduct.Price;
            existingProduct.StockQuantity = updateProductRequest.StockQuantity ?? existingProduct.StockQuantity;
            existingProduct.DiscountPrice = updateProductRequest.DiscountPrice ?? existingProduct.DiscountPrice;
            existingProduct.ReleaseDate = updateProductRequest.ReleaseDate ?? existingProduct.ReleaseDate;
            existingProduct.SKU = updateProductRequest.SKU ?? existingProduct.SKU;
            existingProduct.Manufacturer = updateProductRequest.Manufacturer ?? existingProduct.Manufacturer;
            existingProduct.ProductDimensions = updateProductRequest.ProductDimensions ?? existingProduct.ProductDimensions;
            existingProduct.Weight = updateProductRequest.Weight ?? existingProduct.Weight;
            existingProduct.ImageUrl = "";

            // Update category (if provided)
            if (updateProductRequest.ProductCategoryId.HasValue)
            {
                existingProduct.ProductCategoryId = updateProductRequest.ProductCategoryId.Value;
            }

            // Update IsFeatured field
            existingProduct.IsFeatured = updateProductRequest.IsFeatured ?? existingProduct.IsFeatured;

            // Update the modified date
            existingProduct.ModifiedDate = DateTime.UtcNow;

            var updateResult = await _unitOfWork.ProductRepository.UpdateProductAsync(existingProduct);
            if (updateResult == 0)
            {
                return Result.Failure(ProductErrorMessage.ProductUpdateFailed());
            }

            return Result.SuccessWithObject(new { Id = existingProduct.Id });
        }




        // Delete Product
        public async Task<Result> DeleteProductAsync(Guid id)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return Result.Failure(ProductErrorMessage.ProductNotFound());
            }

            var deleteResult = await _unitOfWork.ProductRepository.DeleteProductAsync(id);
            if (deleteResult == 0)
            {
                return Result.Failure(ProductErrorMessage.ProductDeletionFailed());
            }

            return Result.SuccessWithObject(deleteResult);
        }
    }
}
