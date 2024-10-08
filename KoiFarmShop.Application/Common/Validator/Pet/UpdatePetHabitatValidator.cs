using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePetHabitat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Pet
{
    public class UpdatePetHabitatValidator : PetValidator<UpdatePetHabitatRequest>
    {
        public UpdatePetHabitatValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddPetHabitatTypeRules(request => request.HabitatType);
        }
    }
}
