using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Async
{
    public class PetApp
    {
        private readonly IPetServiceLogic _petServiceLogic;

        public PetApp(IPetServiceLogic petServiceLogic)
        {
            _petServiceLogic = petServiceLogic;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Start Async Demo with Pets...");

            var result = await _petServiceLogic.GetAllPetAsync();
            var pets = result.Object as List<Pet>;

            if (pets == null || !pets.Any())
            {
                Console.WriteLine("No pets found!");
                return;
            }

            Console.WriteLine("\nRun Synchronously:");
            await RunSynchronousDemo(pets);

            Console.WriteLine("\nRun Asynchronously:");
            await RunAsynchronousDemo(pets);
        }

        private async Task RunSynchronousDemo(IList<Pet> pets)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach (var pet in pets)
            {
                await ProcessPet(pet);
            }
            stopwatch.Stop();
            Console.WriteLine($"Synchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
        }

        private async Task RunAsynchronousDemo(IList<Pet> pets)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var tasks = pets.Select(pet => ProcessPet(pet));
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"Asynchronous processing completed in: {stopwatch.Elapsed.TotalSeconds:F2} seconds\n");
        }

        private async Task ProcessPet(Pet pet)
        {
            Console.WriteLine($"Processing: {pet.Name}");
            await Task.Delay(1000); // Simulating async processing
            Console.WriteLine($"Completed: {pet.Name}");
        }
    }
}
