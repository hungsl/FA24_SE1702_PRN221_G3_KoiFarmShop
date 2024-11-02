using FluentValidation;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Abstract
{
    public abstract class UserValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;
        public UserValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Validation for UserName (with option to check existence)
        protected void AddUserNameRules(Expression<Func<T, string>> userNameExpression, bool checkExists = false)
        {
            RuleFor(userNameExpression)
                .NotEmpty().WithState(_ => (UserErrorMessage.FieldIsEmpty("User name")))
                .MinimumLength(3).WithState(_ => UserErrorMessage.UserNameInValidLength());

            // Check if username exists only if required (for registration)
            if (checkExists)
            {
                RuleFor(userNameExpression)
                    .MustAsync(async (userName, cancellation) =>
                        !(await _unitOfWork.UserRepository.UserNameExistsAsync(userName)))
                    .WithState(_ => UserErrorMessage.UserNameIsExist());
            }
        }

        // Validation for Email (with option to check existence)
        protected void AddEmailRules(Expression<Func<T, string>> emailExpression, bool checkExists = false)
        {
            RuleFor(emailExpression)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("Email"))
                .EmailAddress().WithState(_ => UserErrorMessage.EmailInValidFormat());

            // Check if email exists only if required (for registration)
            if (checkExists)
            {
                RuleFor(emailExpression)
                    .MustAsync(async (email, cancellation) =>
                        !(await _unitOfWork.UserRepository.EmailExistsAsync(email)))
                    .WithState(_ => UserErrorMessage.EmailIsExist());
            }
        }

        // Shared validation for Password
        protected void AddPasswordRules(Expression<Func<T, string>> passwordFunc)
        {
            RuleFor(passwordFunc)
                .NotEmpty().WithState(_ => UserErrorMessage.FieldIsEmpty("Password"))
                .MinimumLength(8).WithState(_ => UserErrorMessage.PasswordInValidLength())
                .Matches(@"^[A-Z].*").WithState(_ => UserErrorMessage.PasswordInValidUppercase())
                .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithState(_ => UserErrorMessage.PasswordInValidSpecialChar());
        }
    }
}
