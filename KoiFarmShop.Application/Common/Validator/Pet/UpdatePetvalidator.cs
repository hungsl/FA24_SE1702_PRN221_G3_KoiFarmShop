using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Pet.UpdatePet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Application.Common.Validator.Pet
{
    public class UpdatePetvalidator : PetValidator<UpdatePetRequest>
    {
        public UpdatePetvalidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddPetNameRules(request => request.Name);
            AddPetAgeRules(request => request.Age);
            AddPetGenderRules(request => request.Gender);
            AddPetLengthRules(request => request.Length);
            AddPetWeightRules(request => request.Weight);
        }
    }
}
