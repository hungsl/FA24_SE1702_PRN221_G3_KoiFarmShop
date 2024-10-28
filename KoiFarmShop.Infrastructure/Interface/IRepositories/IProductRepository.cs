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
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<(int totalItems, List<Product> products)> GetAllProductsWithSearchAsync(string searchTerm, string searchField, int pageIndex, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, decimal? minDiscountPrice = null, decimal? maxDiscountPrice = null);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<int> DeleteProductAsync(Guid id);
        Task<int> UpdateProductAsync(Product product);
        Task<List<Product>> GetByIdsAsync(List<Guid> productIds);

        Task<bool> ProductNameExistsAsync(string productName);
    }
}
