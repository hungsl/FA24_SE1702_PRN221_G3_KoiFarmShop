using FluentValidation;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace KoiFarmShop.Application.Common.Validator.Abstract
{
    public class AppointmentForComboValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;
        public AppointmentForComboValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        protected void MakeAppointmentForServiceRules(Expression<Func<T, Guid>> comboServiceNameExpression, string name)
        {
            RuleFor(comboServiceNameExpression)
                .NotEmpty().WithState(_ => (AppointmentErrorMessage.FieldIsEmpty(name)));
        }
    }
}
