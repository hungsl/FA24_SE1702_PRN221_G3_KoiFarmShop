using KoiFarmShop.Application.Common.Result;
using KVSC.Infrastructure.DTOs.Rating.AddRating;
using KVSC.Infrastructure.DTOs.Rating.GetRating;
using KVSC.Infrastructure.DTOs.Rating.UpdateRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Application.Interface.IService
{
    public interface IRatingService
    {
        Task<Result> CreateRatingAsync(AddRatingRequest addRatingRequest);
        Task<Result> GetRatingsByServiceIdAsync(Guid serviceId);
        Task<Result> GetAllRatingsAsync(GetAllRatingsRequest request);
        Task<Result> UpdateRatingAsync(UpdateRatingRequest updateRatingRequest);
        Task<Result> DeleteRatingAsync(Guid id);
    }
}
