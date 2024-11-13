using KoiFarmShop.Application.Interface.IService;
using KVSC.Application.Interface.IService;
using KVSC.Domain.Entities;
using KVSC.Infrastructure.DTOs.Rating.GetRating;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class RatingViewModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IRatingService _ratingService;
        public RatingViewModel(IPetServiceService petServiceService, IRatingService ratingService)
        {
            _petServiceService = petServiceService;
            _ratingService = ratingService;
        }
        public List<GetRatingResponse> Ratings { get; set; }
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }
            // Get all rating
            var ratingResult = await _ratingService.GetRatingsByServiceIdAsync(id);
            if (ratingResult.IsSuccess)
            {
                Ratings = ratingResult.Object as List<GetRatingResponse> ?? new List<GetRatingResponse>();
            }
            else
            {
                Ratings = new List<GetRatingResponse>();
            }

            return Page();
        }
    }
}
