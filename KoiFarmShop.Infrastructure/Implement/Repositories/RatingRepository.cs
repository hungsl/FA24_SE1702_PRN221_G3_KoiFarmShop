using Azure.Core;
using Google;
using KoiFarmShop.Infrastructure.DB;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Rating.GetRating;
using KVSC.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KVSC.Infrastructure.Implement.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly KVSCContext _context;

        public RatingRepository(KVSCContext context)
        {
            _context = context;
        }

        public async Task<Rating> CreateRatingAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> GetRatingByCustomerAndServiceAsync(Guid customerId, Guid serviceId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.CustomerId == customerId && r.ServiceId == serviceId);
        }

        public async Task<List<Rating>> GetByServiceIdAsync(Guid serviceId)
        {
            return await _context.Ratings
                .Where(r => r.ServiceId == serviceId && !r.IsDeleted)
                .ToListAsync();
        }
        public async Task<(int TotalCount, List<Rating> Ratings)> GetAll(GetAllRatingsRequest request)
        {
            var ratingsQuery = _context.Ratings.Where(r => !r.IsDeleted).AsQueryable();

            if (request.ServiceId.HasValue)
            {
                ratingsQuery = ratingsQuery.Where(r => r.ServiceId == request.ServiceId.Value);
            }

            if (request.Score.HasValue)
            {
                ratingsQuery = ratingsQuery.Where(r => r.Score == request.Score.Value);
            }

            if (request.CreatedDate.HasValue)
            {
                ratingsQuery = ratingsQuery.Where(r => r.CreatedDate.Date == request.CreatedDate.Value.Date);
            }

            var totalRatings = await ratingsQuery.CountAsync();

            var ratings = await ratingsQuery
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
                .ToListAsync();
            return (totalRatings, ratings);
        }
        public async Task<Rating> GetByIdAsync(Guid id)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }
        public async Task<int> UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteRatingAsync(Guid id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                rating.IsDeleted = true;
                rating.ModifiedDate = DateTime.UtcNow; // Optional: Track deletion timestamp
                _context.Ratings.Update(rating);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
