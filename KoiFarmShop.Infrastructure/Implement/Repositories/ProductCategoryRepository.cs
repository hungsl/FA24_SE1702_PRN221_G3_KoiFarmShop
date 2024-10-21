using Google.Api.Gax.ResourceNames;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.Implement.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(KVSCContext context) : base(context) { }

        #region bool
        public async Task<bool> ProductCategoryNameExistsAsync(string productCategoryName)
        {
            // Check if any product exists with the specified product name
            return await _context.ProductCategories.AnyAsync(x => x.Name == productCategoryName);
        }
        #endregion

        #region Product CRUD Methods

        // Create new product
        public async Task<int> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            return await _context.SaveChangesAsync();
        }

        // Get product by ID
        public async Task<ProductCategory> GetProductCategoryByIdAsync(Guid productCategoryId)
        {
            return await _context.ProductCategories.FindAsync(productCategoryId);
        }

        // Get all products
        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        // Update product
        public async Task<int> UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Update(productCategory);
            return await _context.SaveChangesAsync();
        }

        // Remove product by entity
        public async Task<bool> RemoveProductCategoryAsync(ProductCategory productCategory)
        {
            _context.ProductCategories.Remove(productCategory);
            return await _context.SaveChangesAsync() > 0;
        }

        // Remove product by ID
        public async Task<bool> RemoveProductCategoryByIdAsync(Guid productCategoryId)
        {
            var product = await GetProductCategoryByIdAsync(productCategoryId);
            if (product != null)
            {
                _context.ProductCategories.Remove(product);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        #endregion
    }


}
