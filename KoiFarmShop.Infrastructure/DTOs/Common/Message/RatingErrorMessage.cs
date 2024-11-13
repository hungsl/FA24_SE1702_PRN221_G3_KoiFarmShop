using KoiFarmShop.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Common.Message
{
    public static class RatingErrorMessage
    {
        public static Error FieldIsEmpty(string fieldName)
            => Error.Validation("Rating.Empty", $"The '{fieldName}' is required.");

        public static Error InvalidFieldValue(string fieldName)
            => Error.Validation("Rating.Invalid", $"'{fieldName}' is not exist.");

        public static Error ExceedsMaxLength(string fieldName, int maxLength)
            => Error.Validation("Rating.MaxLength", $"The '{fieldName}' cannot exceed {maxLength} characters.");
        public static Error ExceedsMinLength(string fieldName, int minLength)
            => Error.Validation("Rating.MinLength", $"The '{fieldName}' must be at least {minLength} characters.");

        public static Error RatingNotExist()
            => Error.NotFound("Rating.NotExist", "Rating does not exist.");
        public static Error RatingNotCreated()
           => Error.Validation("Rating.NotCreated", "Rating could not be created.");
        public static Error RatingNotFound() =>
       Error.NotFound("Rating.NotFound", "Rating not found.");
        public static Error RatingUpdateFailed() =>  Error.Failure("Rating.UpdateFailed", "Failed to update the rating.");
        public static Error RatingDeleteFailed() => Error.Failure("Rating.DeleteFailed", "Failed to delete the rating.");
    }
}
