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
    public abstract class PetValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;
        public PetValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected void AddPetNameRules(Expression<Func<T, string>> petNameExpression)
        {
            RuleFor(petNameExpression)
                .NotEmpty().WithState(_ => (PetErrorMessage.FieldIsEmpty("Pet name")));
        }
    }
}
