using KoiFarmShop.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Common.Message
{
    public static class ProductCategoryErrorMessage
    {
        public static Error FieldIsEmpty(string nameField)
            => Error.Validation("ProductCategory.Empty", $"The '{nameField}' is required.");

        public static Error CategoryNotExist()
            => Error.NotFound("ProductCategory.Exist", $"Product category does not exist.");

        public static Error CategoryNotCreated()
            => Error.Conflict("ProductCategory.Create", $"Product category is not created.");

        public static Error CategoryNameInvalidLength()
            => Error.Validation("ProductCategory.Name.Length", $"Category name must be at least 3 characters.");

        public static Error CategoryNameIsExist()
            => Error.Validation("ProductCategory.Name.Exist", $"The category name already exists.");

        public static Error CategoryDescriptionInvalidLength()
            => Error.Validation("ProductCategory.Description.Length", $"Category description must not exceed 500 characters.");

        public static Error CategoryUpdateFailed()
            => Error.Conflict("ProductCategory.Update.Failed", $"Failed to update the product category.");

        public static Error CategoryNotFound()
            => Error.NotFound("ProductCategory.NotFound", $"The category with the given ID was not found.");

        public static Error CategoryDeletionFailed()
            => Error.Conflict("ProductCategory.Delete.Failed", $"Failed to delete the product category.");

        public static Error CategoryCreatedFailed()
            => Error.Conflict("ProductCategory.Create.Failed", $"Failed to create the product category.");
    }
}
