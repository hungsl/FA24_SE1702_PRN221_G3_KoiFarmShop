using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.RazorWebApp.Pages.KoiService
{
    public class SignalRPetServiceModel : PageModel
    {
        private readonly IPetServiceService _petServiceService;

        public SignalRPetServiceModel(IPetServiceService petServiceService)
        {
            _petServiceService = petServiceService;
        }

        public List<PetService> PetServices { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _petServiceService.GetAllPetServicesAsync();
            if (result.IsSuccess)
            {
                PetServices = result.Object as List<PetService>;
            }
            else
            {
                PetServices = new List<PetService>
                {
                }; 
            }
        }
    }
}
