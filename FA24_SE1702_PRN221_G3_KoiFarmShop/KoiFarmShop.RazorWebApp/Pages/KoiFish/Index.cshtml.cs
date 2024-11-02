using Microsoft.AspNetCore.Mvc.RazorPages;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Application.Implement.Service;
using Microsoft.AspNetCore.Mvc;
using KoiFarmShop.Infrastructure.DTOs.Common;
using KoiFarmShop.Application.Interface.IService;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class IndexModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public IndexModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

        public ResultSearch<Pet> Pet { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string searchName { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string searchColor { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string searchNote { get; set; } = string.Empty;
        public async Task OnGetAsync()
        {
            var result = await _petServiceLogic.GetSearchPetAsync(searchName, searchColor, searchNote);
            if (result.IsSuccess)
            {
                Pet = result.Object as ResultSearch<Pet>;
            }
            else
            {
                Pet = new ResultSearch<Pet>
                {
                    Items = new List<Pet>()
                };
            }
        }
    }
}
