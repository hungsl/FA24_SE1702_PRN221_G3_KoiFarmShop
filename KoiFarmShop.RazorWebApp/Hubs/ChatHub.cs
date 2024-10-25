using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace KoiFarmShop.RazorWebApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IPetServiceService _petServiceService;

        public ChatHub(IPetServiceService petServiceService)
        {
            _petServiceService = petServiceService;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        //public async Task SendService(PetService service)
        //{
        //    await Clients.All.SendAsync("ReceiveService", new
        //    {
        //        service.Name,
        //        service.BasePrice,
        //        service.Duration,
        //        service.AvailableFrom,
        //        service.AvailableTo
        //    });
        //}
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
