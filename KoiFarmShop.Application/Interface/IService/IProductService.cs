    using KVSC.Infrastructure.DTOs.Product.AddProduct;
    using System.Threading.Tasks;
    using KVSC.Infrastructure.DTOs.Product.UpdateProduct;
    using KVSC.Infrastructure.DTOs.Product.SearchProduct;
    using KoiFarmShop.Application.Common.Result;

    namespace KVSC.Application.Interface.IService
    {
        public interface IProductService
        {
            Task<Result> CreateProductAsync(AddProductRequest productRequest);
            Task<Result> GetAllProductsAsync();
            Task<Result> GetAllProductsAsync(string searchTerm, string searchField, int pageIndex, int pageSize, decimal? minPrice = null, decimal? maxPrice = null, decimal? minDiscountPrice = null, decimal? maxDiscountPrice = null);
            Task<Result> GetProductByIdAsync(Guid id);
            Task<Result> UpdateProductAsync(Guid id, UpdateProductRequest productRequest);
            Task<Result> DeleteProductAsync(Guid id);
        }
    }

