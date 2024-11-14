using KoiFarmShop.Infrastructure.Common;
using KVSC.Application.Common.Validator.Abstract;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using KVSC.Infrastructure.DTOs.ProductCategory.AddProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Common.Validator.ProductCategory
{
    public class AddProductCategoryValidator : ProductCategoryValidator<AddProductCategoryRequest>
    {
        public AddProductCategoryValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddCategoryNameRules(request => request.Name, checkExists: true);
            AddCategoryDescriptionRules(descriptionExpression => descriptionExpression.Description);
        }
    }
}
