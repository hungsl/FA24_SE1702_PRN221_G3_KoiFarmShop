using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePetType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Pet
{
    public class UpdatePetTypeValidator : PetValidator<UpdatePetTypeRequest>
    {
        public UpdatePetTypeValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddPetTypeGeneralRules(request => request.GeneralType);
            AddPetTypeSpecificRules(request => request.SpecificType);
        }

    }
}
