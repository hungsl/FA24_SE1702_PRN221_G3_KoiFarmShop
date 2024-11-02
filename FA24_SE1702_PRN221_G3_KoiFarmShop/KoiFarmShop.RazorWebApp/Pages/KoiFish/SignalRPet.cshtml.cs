using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiFish
{
    public class SignalRPetModel : PageModel
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public SignalRPetModel(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }
        public List<Pet> Pets { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _petServiceLogic.GetAllPetAsync();
            if (result.IsSuccess)
            {
                Pets = result.Object as List<Pet>;
            }
            else
            {
                Pets = new List<Pet>
                {
                };
            }
        }
    }
}
