using KoiFarmShop.Infrastructure.Common;
using KVSC.Application.Common.Validator.Abstract;
using KVSC.Infrastructure.DTOs.Rating.AddRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Common.Validator.Rating
{
    public class AddRatingValidator : RatingValidator<AddRatingRequest>
    {
        public AddRatingValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddServiceIdRules(request => request.ServiceId);
            AddCustomerIdRules(request => request.CustomerId);
            AddScoreRules(request => request.Score);
            AddFeedbackRules(request => request.Feedback);
        }
    }
}
