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

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class IndexModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceCategoryService _petCategoryService;
        public IndexModel(IPetServiceService petServiceService, IPetServiceCategoryService petCategoryService)
        {
            _petServiceService = petServiceService;
            _petCategoryService = petCategoryService;
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
        public int PageSize { get; set; } = 10;

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
    }
}
