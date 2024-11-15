using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.User.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.User
{
    public class RegisterValidator : UserValidator<RegisterRequest>
    {
        public RegisterValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            // Check if the email and username already exist for registration
            AddEmailRules(request => request.Email, checkExists: true);
            AddUserNameRules(request => request.UserName, checkExists: true);
            AddPasswordRules(request => request.Password);
        }
    }
}
