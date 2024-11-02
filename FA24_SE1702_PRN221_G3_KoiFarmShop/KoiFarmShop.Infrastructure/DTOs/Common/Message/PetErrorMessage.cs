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
            => Error.Validation("Pet.Empty", $"The '{nameField}' is required.");

        public static Error InvalidFieldValue(string nameField)
            => Error.Validation("Pet.InvalidValue", $"The '{nameField}' field has an invalid value.");

        public static Error PetCreateFailed()
            => Error.Validation("Pet.CreateFailed", "Failed to create pet.");

        public static Error PetUpdateFailed()
            => Error.Validation("Pet.UpdateFailed", "Failed to update pet.");

        public static Error PetDeleteFailed()
           => Error.Validation("Pet.DeleteFailed", "Failed to delete pet.");

        public static Error PetNotFound()
            => Error.NotFound("Pet.NotFound", "The specified pet was not found.");
    }
}
