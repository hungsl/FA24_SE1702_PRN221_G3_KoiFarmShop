using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TaskService _taskService;
        private readonly IPetServiceService _petServiceService;

        public Worker(ILogger<Worker> logger, TaskService taskService, IPetServiceService petServiceService)
        {
            _logger = logger;
            _taskService = taskService;
            _petServiceService = petServiceService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

            do
            {
                _logger.LogInformation("Checking for services expiring soon...");
                List<PetService> expiringServices = await _petServiceService.GetServicesExpiringSoonAsync();

                foreach (var service in expiringServices)
                {
                    _logger.LogInformation($"Service '{service.Name}' is expiring soon. Processing...");
                    await ProcessExpiringServiceAsync(service);
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);  // Kiểm tra lại mỗi 5 giây
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        private async Task ProcessExpiringServiceAsync(PetService service)
        {
            _logger.LogInformation($"Notifying about service '{service.Name}'.");
            await Task.Delay(500);
            service.IsDeleted = true; 
            service.ModifiedDate = DateTime.UtcNow;

            await _petServiceService.UpdatePetServiceAsync(service);
        }
    }
}
