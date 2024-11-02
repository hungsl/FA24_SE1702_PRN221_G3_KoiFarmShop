using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.PetService
{
    public class AddPetServiceCategoryValidator : PetServiceCategoryValidator<AddPetServiceCategoryRequest>
    {
        public AddPetServiceCategoryValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddCategoryNameRules(request => request.Name);
            AddDescriptionRules(request => request.Description);
            AddServiceTypeRules(request => request.ServiceType);
            AddApplicableToRules(request => request.ApplicableTo);
        }
    }
}
