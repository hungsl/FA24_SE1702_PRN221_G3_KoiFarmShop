using KoiFarmShop.Infrastructure.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.DTOs.Common.Message
{
    public static class ProductErrorMessage
    {
        public static Error FieldIsEmpty(string nameField)
            => Error.Validation("Product.Empty", $"The '{nameField}' is required.");

        public static Error ProductNotExist()
            => Error.NotFound("Product.Exist", $"Product does not exist.");

        public static Error ProductNotCreated()
            => Error.Conflict("Product.Create", $"Product is not created.");

        public static Error ProductNameInValidLength()
            => Error.Validation("Product.Name.Length", $"Product name must be at least 3 characters.");

        public static Error ProductNameIsExist()
            => Error.Validation("Product.Name.Exist", $"The product name already exists.");

        public static Error ProductPriceInvalid()
            => Error.Validation("Product.Price.Invalid", $"Product price must be greater than zero.");

        public static Error ProductDescriptionInvalidLength()
            => Error.Validation("Product.Description.Length", $"Product description must not exceed 500 characters.");

        public static Error ProductUpdateFailed()
            => Error.Conflict("Product.Update.Failed", $"Failed to update the product.");

        public static Error ProductNotFound()
           => Error.NotFound("Product.NotFound", $"The product with the given ID was not found.");

        public static Error ProductDeletionFailed()
            => Error.Conflict("Product.Delete.Failed", $"Failed to delete the product.");
    }
}


