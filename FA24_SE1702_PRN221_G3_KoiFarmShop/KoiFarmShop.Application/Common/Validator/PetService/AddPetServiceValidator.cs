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
    public class AddPetServiceValidator : PetServiceValidator<AddPetServiceRequest>
    {
        public AddPetServiceValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddPetServiceNameRules(request => request.Name);
            AddBasePriceRules(request => request.BasePrice);
            AddDurationRules(request => request.Duration);
            AddTravelCostRangeRules(request => request.TravelCost);
            AddDateRangeRules(request => request.AvailableFrom, request => request.AvailableTo);
        }
    }
}
