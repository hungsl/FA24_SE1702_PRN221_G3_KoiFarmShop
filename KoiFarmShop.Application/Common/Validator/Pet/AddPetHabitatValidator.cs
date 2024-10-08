using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPetHabitat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Pet
{
    public class AddPetHabitatValidator : PetValidator<AddPetHabitatRequest>
    {
        public AddPetHabitatValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddPetHabitatTypeRules(request => request.HabitatType);
        }
    }
}
