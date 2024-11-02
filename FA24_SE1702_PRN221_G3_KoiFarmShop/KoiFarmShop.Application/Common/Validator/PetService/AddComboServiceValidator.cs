using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.PetService
{
    public class AddComboServiceValidator : ComboServiceValidator<AddComboServiceRequest>
    {
        public AddComboServiceValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddComboServiceNameRules(request => request.Name);
            AddDiscountPercentageRules(request => request.DiscountPercentage);
            AddServiceItemsRules(request => request.ServiceIds);
        }
    }
}
