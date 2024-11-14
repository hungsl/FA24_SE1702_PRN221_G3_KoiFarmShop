using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Product.SearchProduct;
using KVSC.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.Implement.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(KVSCContext context) : base(context) { }

        //#region bool
        //public async Task<bool> ProductNameExistsAsync(string productName)
        //{
        //    // Check if any product exists with the specified product name
        //    return await _context.Products.AnyAsync(x => x.Name == productName);
        //}
        //#endregion

        //#region Product CRUD Methods

        //// Create new product
        //public async Task<int> CreateProductAsync(Product product)
        //{
        //    _context.Products.Add(product);
        //    return await _context.SaveChangesAsync();
        //}

        //// Get product by ID
        //public async Task<Product> GetProductByIdAsync(Guid productId)
        //{
        //    return await _context.Products.FindAsync(productId);
        //}

        //// Get all products
        //public async Task<List<Product>> GetAllProductsAsync()
        //{
        //    return await _context.Products.ToListAsync();
        //}

        //// Update product
        //public async Task<int> UpdateProductAsync(Product product)
        //{
        //    _context.Products.Update(product);
        //    return await _context.SaveChangesAsync();
        //}

        //// Remove product by entity
        //public async Task<bool> RemoveProductAsync(Product product)
        //{
        //    _context.Products.Remove(product);
        //    return await _context.SaveChangesAsync() > 0;
        //}

        //// Remove product by ID
        //public async Task<bool> RemoveProductByIdAsync(Guid productId)
        //{
        //    var product = await GetProductByIdAsync(productId);
        //    if (product != null)
        //    {
        //        _context.Products.Remove(product);
        //        return await _context.SaveChangesAsync() > 0;
        //    }
        //    return false;
        //}

        //#endregion

        //#region Product-specific methods
        //// Add more specific methods for Product entity here if needed
        //public async Task<SearchProductResponse> GetProductsAsync(SearchProductRequest request)
        //{
        //    var response = new SearchProductResponse
        //    {
        //        Error = Error.None // Initialize Error to None
        //    };

        //    try
        //    {
        //        var productsQuery = _context.Products.AsQueryable();

        //        // Filter Query
        //        if (!string.IsNullOrWhiteSpace(request.FilterOn) && !string.IsNullOrWhiteSpace(request.FilterQuery))
        //        {
        //            switch (request.FilterOn.Trim().ToLower())
        //            {
        //                case "name":
        //                    // Use EF.Functions.Like for case-insensitive comparison
        //                    productsQuery = productsQuery.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.FilterQuery.ToLower()}%"));
        //                    break;
        //                case "description":
        //                    // Use EF.Functions.Like for case-insensitive comparison
        //                    productsQuery = productsQuery.Where(x => EF.Functions.Like(x.Description.ToLower(), $"%{request.FilterQuery.ToLower()}%"));
        //                    break;
        //                case "category":
        //                    productsQuery = productsQuery.Where(x => x.ProductCategoryId.ToString() == request.FilterQuery); // Adjust as needed
        //                    break;
        //            }
        //        }

        //        // Price range
        //        if (request.FromPrice.HasValue)
        //        {
        //            productsQuery = productsQuery.Where(x => x.Price >= request.FromPrice.Value);
        //        }
        //        if (request.ToPrice.HasValue)
        //        {
        //            productsQuery = productsQuery.Where(x => x.Price <= request.ToPrice.Value);
        //        }

        //        // Sorting
        //        var validSortProperties = new Dictionary<string, Expression<Func<Product, object>>>
        //{
        //    { "name", p => p.Name },
        //    { "price", p => p.Price },
        //    { "stockquantity", p => p.StockQuantity },
        //    { "imageurl", p => p.ImageUrl }
        //};

        //        // Sorting
        //        if (!string.IsNullOrEmpty(request.SortBy))
        //        {
        //            productsQuery = request.IsAscending == true
        //                ? productsQuery.OrderBy(x => EF.Property<object>(x, request.SortBy))
        //                : productsQuery.OrderByDescending(x => EF.Property<object>(x, request.SortBy));
        //        }


        //        // Pagination validation
        //        if (request.PageNumber < 1) request.PageNumber = 1;
        //        if (request.PageSize < 1) request.PageSize = 10;

        //        // Pagination
        //        response.TotalCount = await productsQuery.CountAsync();
        //        var products = await productsQuery.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

        //        // Mapping products to ProductDTO
        //        response.Products = products.Select(product => new SearchProductResponse.ProductDTO
        //        {
        //            Name = product.Name,
        //            Price = product.Price,
        //            StockQuantity = product.StockQuantity,
        //            ImageUrl = product.ImageUrl
        //        }).ToList();

        //        // Set the error to None indicating success
        //        response.Error = Error.None;
        //    }
        //    catch (Exception e)
        //    {
        //        // Log and set meaningful error codes
        //        response.Error = Error.Failure("ProductFetchError", "An error occurred while fetching the products.");
        //    }

        //    return response;
        //}

        public async Task<bool> ProductNameExistsAsync(string productName)
        {
            return await _context.Products.AnyAsync(p => p.Name == productName && !p.IsDeleted);
        }


        //#endregion
        // CREATE
        public async Task<Product> CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        // READ
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(i => i.ProductCategory).Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task<(int totalItems, List<Product> products)> GetAllProductsWithSearchAsync(string searchTerm, string searchField, int pageIndex, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, decimal? minDiscountPrice = null, decimal? maxDiscountPrice = null)
        {
            var query = _context.Set<Product>().AsQueryable();
            query = query.Where(p => !p.IsDeleted);

            // Search by the selected field (Name, Description, ProductDimensions)
            if (!string.IsNullOrEmpty(searchTerm))
            {
                switch (searchField)
                {
                    case "Name":
                        query = query.Where(p => p.Name.Contains(searchTerm));
                        break;
                    case "Description":
                        query = query.Where(p => p.Description.Contains(searchTerm));
                        break;
                    case "ProductDimensions":
                        query = query.Where(p => p.ProductDimensions.Contains(searchTerm));
                        break;
                }
            }

            // Filter by price range
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            // Filter by discount price range
            if (minDiscountPrice.HasValue)
            {
                query = query.Where(p => p.DiscountPrice >= minDiscountPrice.Value);
            }
            if (maxDiscountPrice.HasValue)
            {
                query = query.Where(p => p.DiscountPrice <= maxDiscountPrice.Value);
            }

            // Paging
            var totalItems = await query.CountAsync();
            var products = await query
                                    .Skip((pageIndex - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return (totalItems, products);
        }



        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        // UPDATE
        public async Task<int> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync();
        }

        // DELETE (soft delete)
        public async Task<int> DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDeleted = true;
                _context.Products.Update(product);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<List<Product>> GetByIdsAsync(List<Guid> productIds)
        {
            return await _context.Products
                                 .Where(p => productIds.Contains(p.Id))
                                 .ToListAsync();
        }




    }
}
