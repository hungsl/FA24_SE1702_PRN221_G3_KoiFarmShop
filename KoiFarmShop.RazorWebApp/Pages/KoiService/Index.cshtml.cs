using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Application.Interface.IService;
using System.Linq.Dynamic.Core;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KVSC.Application.Interface.IService;
using KVSC.Infrastructure.DTOs.Rating.AddRating;

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class IndexModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceCategoryService _petCategoryService;
        private readonly IRatingService _ratingService;
        public IndexModel(IPetServiceService petServiceService, IPetServiceCategoryService petCategoryService, IRatingService ratingService)
        {
            _petServiceService = petServiceService;
            _petCategoryService = petCategoryService;
            _ratingService = ratingService;
        }

        public PagedResultSearch<PetService> PetService { get;set; } = default!;
        [BindProperty(SupportsGet = true)]

        public string SearchName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SearchDuration { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SearchCategoryName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        [BindProperty]
        public AddRatingRequest RatingRequest { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _petServiceService.GetAllPetServicesAsync(SearchName, SearchDuration, SearchCategoryName, PageIndex, PageSize);
            if (result.IsSuccess)
            {
                // Chuyển Object thành danh sách PetService
                PetService = result.Object as PagedResultSearch<PetService> ?? new PagedResultSearch<PetService>
                {
                    TotalItems = 0,
                    Items = new List<PetService>()
                };
            }
            else
            {
                PetService = new PagedResultSearch<PetService>
                {
                    TotalItems = 0,
                    Items = new List<PetService>()
                };
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var result = await _ratingService.CreateRatingAsync(RatingRequest);

            if (result.IsSuccess)
            {
                TempData["SuccessMessageRating"] = "Rating successfully!";
                return RedirectToPage();
            }
            else
            {
                TempData["ErrorMessageRating"] = "Rating fail!";
                return RedirectToPage();
            }
        }
    }
}
