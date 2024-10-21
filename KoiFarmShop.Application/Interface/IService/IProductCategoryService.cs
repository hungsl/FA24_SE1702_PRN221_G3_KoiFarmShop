using KoiFarmShop.Application.Common.Result;
using KVSC.Infrastructure.DTOs.ProductCategory.AddProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.DeleteProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.GetProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.UpdateProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Interface.IService
{
    public interface IProductCategoryService
    {
        Task<Result> AddProductCategoryAsync(AddProductCategoryRequest addProductCategoryRequest);
        Task<Result> GetProductCategoryByIdAsync(GetProductCategoryRequest getProductCategoryRequest);
        Task<Result> UpdateProductCategoryAsync(UpdateProductCategoryRequest updateProductCategoryRequest);
        Task<Result> DeleteProductCategoryAsync(DeleteProductCategoryRequest request);

    }
}
