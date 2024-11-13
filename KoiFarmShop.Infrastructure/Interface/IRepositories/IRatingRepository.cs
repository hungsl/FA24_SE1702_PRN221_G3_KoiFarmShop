using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Rating.GetRating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.Interface.IRepositories
{
    public interface IRatingRepository
    {
        Task<Rating> CreateRatingAsync(Rating rating);
        Task<Rating> GetRatingByCustomerAndServiceAsync(Guid customerId, Guid serviceId);
        Task<List<Rating>> GetByServiceIdAsync(Guid serviceId);
        Task<(int TotalCount, List<Rating> Ratings)> GetAll(GetAllRatingsRequest request);
        Task<Rating> GetByIdAsync(Guid id);
        Task<int> UpdateAsync(Rating rating);
        Task<int> DeleteRatingAsync(Guid id);
    }
}
