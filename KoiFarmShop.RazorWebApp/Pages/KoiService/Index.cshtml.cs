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

        public IndexModel(IPetServiceService petServiceService)
        {
            _petServiceService = petServiceService;
        }

        public PagedResultSearch<PetService> PetService { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync()
        {
            var result = await _petServiceService.GetAllPetServicesAsync(SearchTerm, PageIndex, PageSize);
            if (result.IsSuccess)
            {
                // Chuyển Object thành danh sách PetService
                 PetService = result.Object as PagedResultSearch<PetService>;
            }
            else
            {
                PetService = new PagedResultSearch<PetService>
                {
                    TotalItems = 0,
                    Items = new List<PetService>()
                }; // Xử lý khi thất bại
            }
        }
    }
}
