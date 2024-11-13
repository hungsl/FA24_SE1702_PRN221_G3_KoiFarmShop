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
    public class RatingValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;

        public RatingValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected void AddServiceIdRules(Expression<Func<T, Guid>> serviceIdExpression)
        {
            RuleFor(serviceIdExpression)
                .NotEmpty().WithState(_ => RatingErrorMessage.FieldIsEmpty("ServiceId"))
                .MustAsync(async (id, _) => await _unitOfWork.PetServiceRepository.GetByIdAsync(id) != null)
                .WithState(_ => RatingErrorMessage.InvalidFieldValue("Service"));
        }

        protected void AddCustomerIdRules(Expression<Func<T, Guid>> customerIdExpression)
        {
            RuleFor(customerIdExpression)
                .NotEmpty().WithState(_ => RatingErrorMessage.FieldIsEmpty("CustomerId"))
                .MustAsync(async (id, _) => await _unitOfWork.UserRepository.GetByIdAsync(id) != null)
                .WithState(_ => RatingErrorMessage.InvalidFieldValue("Customer"));
        }

        protected void AddScoreRules(Expression<Func<T, int>> scoreExpression)
        {
            RuleFor(scoreExpression)
                .InclusiveBetween(1, 5).WithState(_ => RatingErrorMessage.InvalidFieldValue("Score"));
        }

        protected void AddFeedbackRules(Expression<Func<T, string>> feedbackExpression)
        {
            RuleFor(feedbackExpression)
                .MinimumLength(3).WithState(_ => RatingErrorMessage.ExceedsMinLength("Feedback", 3))
                .MaximumLength(500).WithState(_ => RatingErrorMessage.ExceedsMaxLength("Feedback", 500));
        }
    }
}
