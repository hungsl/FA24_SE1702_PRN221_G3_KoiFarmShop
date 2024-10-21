using KoiFarmShop.Infrastructure.Common;
using KVSC.Application.Common.Validator.Abstract;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using KVSC.Infrastructure.DTOs.Product.UpdateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Common.Validator.Product
{
    public class UpdateProductValidator : ProductValidator<UpdateProductRequest>
    {
        public UpdateProductValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddProductNameRules(request => request.Name,isRequired: false);
            AddProductPriceRules(priceExpression => priceExpression.Price, isRequired: false);
            AddProductDescriptionRules(descriptionExpression => descriptionExpression.Description, isRequired: false);
        }
    }
}
