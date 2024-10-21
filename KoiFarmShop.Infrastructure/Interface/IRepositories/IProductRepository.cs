using KoiFarmShop.Infrastructure.Interface.IRepositories;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Product.SearchProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.Interface.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>   
    {
        Task<bool> ProductNameExistsAsync(string productName);
        Task<int> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<int> UpdateProductAsync(Product product);
        Task<bool> RemoveProductAsync(Product product);
        Task<bool> RemoveProductByIdAsync(Guid productId);
        Task<SearchProductResponse> GetProductsAsync(SearchProductRequest productName);
    }
}
