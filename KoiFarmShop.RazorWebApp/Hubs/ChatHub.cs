using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace KoiFarmShop.RazorWebApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IPetServiceService _petServiceService;
        private readonly IPetServiceLogic _petServiceLogic;

        public ChatHub(IPetServiceService petServiceService, IPetServiceLogic petServiceLogic)
        {
            _petServiceService = petServiceService;
            _petServiceLogic = petServiceLogic;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendPet(Guid petId)
        {
            var result = await _petServiceLogic.GetPetByIdAsync(petId);

            if (result.IsSuccess && result.Object != null)
            {
                var pet = result.Object as Pet;
                await Clients.All.SendAsync("ReceivePet", new
                {
                    name = pet.Name,
                    age = pet.Age,
                    weight = pet.Weight,
                    lastHealthCheck = pet.LastHealthCheck,
                    healthStatus = pet.HealthStatus,
                    color = pet.Color
                });
            }
        }

        public async Task SendService(Guid serviceId)
        {
            var result = await _petServiceService.GetPetServiceByIdAsync(serviceId);

            if (result.IsSuccess && result.Object != null)
            {
                var service = result.Object as PetService;
                await Clients.All.SendAsync("ReceiveService", new
                {
                    name = service.Name,
                    basePrice = service.BasePrice,
                    duration = service.Duration,
                    availableFrom = service.AvailableFrom,
                    availableTo = service.AvailableTo
                });
            }
        }
    }
}
