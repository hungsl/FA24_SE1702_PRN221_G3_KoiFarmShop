using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DTOs.Common.Message
{
    public static class PetErrorMessage
    {
        //Pet
        public static Error FieldIsEmpty(string nameField)
            => Error.Validation("Pet.Empty", $"The '{nameField}' is required.");

        public static Error FieldMustBePositive(string nameField)
           => Error.Validation("Pet.Positive", $"The '{nameField}' must be a positive value.");

        public static Error FieldLength(string nameField, int minLength, int maxLength)
            => Error.Validation("Pet.Length", $"The '{nameField}' must be between {minLength} and {maxLength} characters long.");

        public static Error InvalidAge(string age)
           => Error.Validation("Pet.InvalidAge", $"The age '{age}' is not valid.");

        public static Error PetCreateFailed()
            => Error.Validation("Pet.CreateFailed", "Failed to create pet.");

        public static Error PetUpdateFailed()
            => Error.Validation("Pet.UpdateFailed", "Failed to update pet.");

        public static Error PetDeleteFailed()
           => Error.Validation("Pet.DeleteFailed", "Failed to delete pet.");

        public static Error PetNotFound()
            => Error.NotFound("Pet.NotFound", "The specified pet was not found.");


        //PetType
        public static Error PetTypeCreateFailed()
            => Error.Validation("Pet.CreateFailed", "Failed to create pet type.");

        public static Error PetTypeUpdateFailed()
            => Error.Validation("Pet.UpdateFailed", "Failed to update pet type.");

        public static Error PetTypeDeleteFailed()
           => Error.Validation("PetType.DeleteFailed", "Failed to delete pet type.");

        public static Error PetTypeNotFound()
            => Error.NotFound("PetType.NotFound", "The pet type not found.");

        //PetHabitat

        public static Error PetHabitatNotFound()
        => Error.NotFound("PetHabitat.NotFound", "Pet habitat not found.");

        public static Error PetHabitatCreateFailed()
            => Error.Validation("PetHabitat.CreateFailed", "Failed to create pet habitat.");

        public static Error PetHabitatUpdateFailed()
            => Error.Validation("PetHabitat.UpdateFailed", "Failed to update pet habitat.");

        public static Error PetHabitatDeleteFailed()
            => Error.Validation("PetHabitat.DeleteFailed", "Failed to delete pet habitat.");
    }
}
