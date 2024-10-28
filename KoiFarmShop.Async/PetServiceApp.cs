using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KoiFarmShop.Async
{
    public class PetServiceApp
    {
        private readonly IPetServiceService _petServiceService;

        public PetServiceApp(IPetServiceService petServiceService)
        {
            _petServiceService = petServiceService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Start Async Demo with PetService...");

            var result = await _petServiceService.GetAllPetServicesAsync();

           var petServices = result.Object as List<PetService>;

            if (petServices == null || !petServices.Any())
            {
                Console.WriteLine("No services found!");
                return;
            }

            Console.WriteLine("\nRun Synchronously:");
            await RunSynchronousDemo(petServices);

            Console.WriteLine("\nRun Asynchronously:");
            await RunAsynchronousDemo(petServices);
        }

        private async Task RunSynchronousDemo(IList<PetService> petServices)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var service in petServices)
            {
                await ProcessPetService(service);
            }
            stopwatch.Stop(); 
            Console.WriteLine($"Synchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
        }

        private async Task RunAsynchronousDemo(IList<PetService> petServices)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var tasks = petServices.Select(service => ProcessPetService(service));
            await Task.WhenAll(tasks);
            stopwatch.Stop(); 
            Console.WriteLine($"Asynchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
        }

        private async Task ProcessPetService(PetService service)
        {
            Console.WriteLine($"Processing: {service.Name}");
            await Task.Delay(1000); 
            Console.WriteLine($"Completed: {service.Name}");
        }
    }
}
