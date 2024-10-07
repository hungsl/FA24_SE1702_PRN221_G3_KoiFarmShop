using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.User
{
    public class LoginValidator : UserValidator<LoginRequest>
    {

        public LoginValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            // No need to check if the email exists, just validate it
            AddEmailRules(request => request.Email, checkExists: false);
            AddPasswordRules(request => request.Password);
        }


    }
}
