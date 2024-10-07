using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Common.Message
{
    public static class PetErrorMessage
    {
        public static Error FieldIsEmpty(string nameField)
            => Error.Validation("User.Empty", $"The '{nameField}' is required.");

    }
}
