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
using KoiFarmShop.Application.Common.Result;

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class IndexModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;

        public IndexModel(IPetServiceService petServiceService)
        {
            _petServiceService = petServiceService;
        }

        public IList<PetService> PetService { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var result = await _petServiceService.GetAllPetServicesAsync();
            if (result.IsSuccess)
            {
                // Chuyển Object thành danh sách PetService
                PetService = result.Object as IList<PetService>;
            }
            else
            {
                PetService = new List<PetService>(); // Xử lý khi thất bại
            }
        }
    }
}
