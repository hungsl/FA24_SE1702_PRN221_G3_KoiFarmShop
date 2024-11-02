using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Common.Message
{
    public static class UserErrorMessage
    {
        public static Error FieldIsEmpty(string nameField)
            => Error.Validation("User.Empty", $"The '{nameField}' is required.");



        public static Error UserNotExist()
            => Error.NotFound("User.Exist", $"Invalid email or password");

        public static Error UserNoCreated()
            => Error.Conflict("User.Exist", $"User is not created!!");

        public static Error UserNameInValidFormat()
            => Error.Validation("User.UserName.Format", $"The username is invalid.");
        public static Error UserNameInValidLength()
            => Error.Validation("User.UserName.Length", $"User name must be at least 3 characters.");
        public static Error UserNameIsExist()
            => Error.Validation("User.UserName.Exist", $"The user name is exist.");


        public static Error EmailInValidFormat()
            => Error.Validation("User.Email.Format", $"The email is invalid.");
        public static Error EmailIsExist()
            => Error.Validation("User.Email.Exist", $"The email is exist.");


        public static Error PasswordInValidLength()
            => Error.Validation("User.Password.Length", $"The password must be at least 3 characters.");
        public static Error PasswordInValidUppercase()
            => Error.Validation("User.Password.Uppercase", $"Password must start with a strong character (uppercase letter).");
        public static Error PasswordInValidSpecialChar()
            => Error.Validation("User.Password.SpecialChar", $"Password must contain at least one special character.");

    }
}
