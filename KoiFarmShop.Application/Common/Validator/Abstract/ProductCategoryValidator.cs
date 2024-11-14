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
    public abstract class ProductCategoryValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;
        public ProductCategoryValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected void AddCategoryNameRules(Expression<Func<T, string>> categoryNameExpression, bool isRequired = true, bool checkExists = false)
        {
            var rule = RuleFor(categoryNameExpression)
                .MinimumLength(3).WithState(_ => ProductCategoryErrorMessage.CategoryDescriptionInvalidLength())
                .When(request => !string.IsNullOrEmpty(categoryNameExpression.Compile()(request))); // Only validate if not null

            if (isRequired)
            {
                rule.NotEmpty().WithState(_ => ProductCategoryErrorMessage.FieldIsEmpty("Category name"));
            }

            // Check if category name exists only if required
            if (checkExists)
            {
                rule.MustAsync(async (categoryName, cancellation) =>
                        !(await _unitOfWork.ProductCategoryRepository.ProductCategoryNameExistsAsync(categoryName)))
                    .WithState(_ => ProductCategoryErrorMessage.CategoryNameIsExist())
                    .When(request => !string.IsNullOrEmpty(categoryNameExpression.Compile()(request))); // Check only if not null
            }
        }

        protected void AddCategoryDescriptionRules(Expression<Func<T, string>> descriptionExpression, bool isRequired = true)
        {
            var rule = RuleFor(descriptionExpression)
                .MaximumLength(500).WithState(_ => ProductCategoryErrorMessage.CategoryDescriptionInvalidLength())
                .When(request => !string.IsNullOrEmpty(descriptionExpression.Compile()(request))); // Only validate if not null

            if (isRequired)
            {
                rule.NotEmpty().WithState(_ => ProductCategoryErrorMessage.FieldIsEmpty("Category description"));
            }
        }
    }
}
