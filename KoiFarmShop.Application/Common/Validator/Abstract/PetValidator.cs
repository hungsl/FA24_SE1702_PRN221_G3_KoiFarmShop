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
                .NotEmpty().WithState(_ => (PetErrorMessage.FieldIsEmpty("Name")));
        }

        protected void AddPetAgeRules(Expression<Func<T, decimal>> petAgeExpression)
        {
            RuleFor(petAgeExpression)
                .GreaterThan(0).WithState(_ => (PetErrorMessage.InvalidFieldValue("Age")));
        }

        protected void AddPetGenderRules(Expression<Func<T, string>> petGenderExpression)
        {
            RuleFor(petGenderExpression)
                .NotEmpty().WithState(_ => (PetErrorMessage.FieldIsEmpty("Gender")));
        }

        protected void AddPetColorRules(Expression<Func<T, string>> petColorExpression)
        {
            RuleFor(petColorExpression)
                .NotEmpty().WithState(_ => (PetErrorMessage.FieldIsEmpty("Color")));
        }

        protected void AddPetImageUrlRules(Expression<Func<T, string>> petImageUrlExpression)
        {
            RuleFor(petImageUrlExpression)
                .NotEmpty().WithState(_ => (PetErrorMessage.FieldIsEmpty("ImageUrl")));
        }

        protected void AddPetLengthRules(Expression<Func<T, double>> petLengthExpression)
        {
            RuleFor(petLengthExpression)
                .GreaterThan(0).WithState(_ => (PetErrorMessage.InvalidFieldValue("Length")));
        }

        protected void AddPetWeightRules(Expression<Func<T, double>> petWeightExpression)
        {
            RuleFor(petWeightExpression)
                .GreaterThan(0).WithState(_ => (PetErrorMessage.InvalidFieldValue("Weight")));
        }

        protected void AddPetQuantityRules(Expression<Func<T, int>> petQuantityExpression)
        {
            RuleFor(petQuantityExpression)
                .GreaterThan(0).WithState(_ => (PetErrorMessage.InvalidFieldValue("Quantity")));
        }
        protected void AddPetNoteRules(Expression<Func<T, string>> petNoteExpression)
        {
            RuleFor(petNoteExpression)
                .NotEmpty().WithState(_ => (PetErrorMessage.FieldIsEmpty("Note")));
        }

        protected void AddPetHealthStatusRules(Expression<Func<T, int>> petHealthStatusExpression)
        {
            RuleFor(petHealthStatusExpression)
                .GreaterThan(0).WithState(_ => (PetErrorMessage.InvalidFieldValue("HealthStatus")));
        }
    }
}
