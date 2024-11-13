using FluentValidation;
using KoiFarmShop.Infrastructure.Common;
using KVSC.Application.Common.Validator.Abstract;
using KVSC.Infrastructure.DTOs.Rating.UpdateRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Common.Validator.Rating
{
    public class UpdateRatingValidator : RatingValidator<UpdateRatingRequest>
    {
        public UpdateRatingValidator(UnitOfWork unitOfWork) : base(unitOfWork)
        {
            AddCustomerIdRules(request => request.CustomerId);
            AddScoreRules(request => request.Score);
            AddFeedbackRules(request => request.Feedback);
        }
    }
}
