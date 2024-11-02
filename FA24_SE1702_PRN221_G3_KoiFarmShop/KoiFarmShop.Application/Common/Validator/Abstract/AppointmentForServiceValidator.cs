using FluentValidation;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Common.Message;
using KoiFarmShop.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Abstract
{
    public class AppointmentForServiceValidator<T> : AbstractValidator<T>
    {
        private readonly UnitOfWork _unitOfWork;
        private IUnitOfWork unitOfWork;

        public AppointmentForServiceValidator(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AppointmentForServiceValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected void MakeAppointmentForServiceRules(Expression<Func<T, Guid>> comboServiceNameExpression, string name)
        {
            RuleFor(comboServiceNameExpression)
                .NotEmpty().WithState(_ => (AppointmentErrorMessage.FieldIsEmpty(name)));
        }
    }
}
