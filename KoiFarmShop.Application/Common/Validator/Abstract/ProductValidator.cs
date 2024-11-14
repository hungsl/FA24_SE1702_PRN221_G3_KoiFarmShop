using FluentValidation;
using KoiFarmShop.Infrastructure.Common;
using KVSC.Infrastructure.DTOs.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Common.Validator.Abstract
{
    public abstract class ProductValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;
        public ProductValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected void AddProductNameRules(Expression<Func<T, string>> productNameExpression, bool isRequired = true, bool checkExists = false)
        {
            var rule = RuleFor(productNameExpression)
                .MinimumLength(3).WithState(_ => ProductErrorMessage.ProductNameInValidLength())
                .When(request => !string.IsNullOrEmpty(productNameExpression.Compile()(request))); // Only validate if not null

            if (isRequired)
            {
                rule.NotEmpty().WithState(_ => ProductErrorMessage.FieldIsEmpty("Product name"));
            }

            // Check if product name exists only if required
            if (checkExists)
            {
                rule.MustAsync(async (productName, cancellation) =>
                        !(await _unitOfWork.ProductRepository.ProductNameExistsAsync(productName)))
                    .WithState(_ => ProductErrorMessage.ProductNameIsExist())
                    .When(request => !string.IsNullOrEmpty(productNameExpression.Compile()(request))); // Check only if not null
            }
        }

        // Validation for Product Price with support for optional fields
        protected void AddProductPriceRules(Expression<Func<T, decimal?>> priceExpression, bool isRequired = true)
        {
            var rule = RuleFor(priceExpression)
                .GreaterThan(0).WithState(_ => ProductErrorMessage.ProductPriceInvalid())
                .When(request => priceExpression.Compile()(request).HasValue); // Only validate if provided

            if (isRequired)
            {
                rule.NotNull().WithState(_ => ProductErrorMessage.FieldIsEmpty("Product price"));
            }
        }

        // Validation for Product Description with support for optional fields
        protected void AddProductDescriptionRules(Expression<Func<T, string>> descriptionExpression, bool isRequired = true)
        {
            var rule = RuleFor(descriptionExpression)
                .MaximumLength(500).WithState(_ => ProductErrorMessage.ProductDescriptionInvalidLength())
                .When(request => !string.IsNullOrEmpty(descriptionExpression.Compile()(request))); // Only validate if not null

            if (isRequired)
            {
                rule.NotEmpty().WithState(_ => ProductErrorMessage.FieldIsEmpty("Product description"));
            }
        }


    }
}
