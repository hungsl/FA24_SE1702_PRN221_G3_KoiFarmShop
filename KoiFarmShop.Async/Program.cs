using System.Diagnostics;

namespace KoiFarmShop.Async
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Start Async Demo with PetService...");

            // Khởi tạo danh sách PetService cứng
            var petServices = GetSamplePetServices();

            // Chạy demo xử lý đồng bộ
            Console.WriteLine("\nRun Synchronously:");
            await RunSynchronousDemo(petServices);

            // Chạy demo xử lý bất đồng bộ
            Console.WriteLine("\nRun Asynchronously:");
            await RunAsynchronousDemo(petServices);
        }

        // Create sample data for PetService
        public static List<PetService> GetSamplePetServices()
        {
            return new List<PetService>
        {
        new PetService { Name = "Fish Bathing", Duration = "30 minutes", AvailableTo = DateTime.UtcNow.AddSeconds(3) },
        new PetService { Name = "Tank Cleaning", Duration = "1 hour", AvailableTo = DateTime.UtcNow.AddSeconds(2) },
        new PetService { Name = "Health Check", Duration = "15 minutes", AvailableTo = DateTime.UtcNow.AddSeconds(1) }
        };
        }

        // Demo of synchronous processing
        public static async Task RunSynchronousDemo(List<PetService> petServices)
        {
            var stopwatch = Stopwatch.StartNew();

            // Process each service sequentially
            foreach (var service in petServices)
            {
                await ProcessPetService(service);
            }

            stopwatch.Stop();
            Console.WriteLine($"Synchronous processing completed in: {stopwatch.ElapsedMilliseconds}ms");
        }

        // Demo of asynchronous processing
        public static async Task RunAsynchronousDemo(List<PetService> petServices)
        {
            var stopwatch = Stopwatch.StartNew();

            // Execute processing tasks concurrently
            var tasks = petServices.Select(service => ProcessPetService(service));
            await Task.WhenAll(tasks);

            stopwatch.Stop();
            Console.WriteLine($"Asynchronous processing completed in: {stopwatch.ElapsedMilliseconds}ms");
        }

        // Simulate processing of a PetService
        public static async Task ProcessPetService(PetService service)
        {
            Console.WriteLine($"Processing service: {service.Name} (Ends at: {service.AvailableTo})...");
            int delayTime = service.Name switch
            {
                "Fish Bathing" => 3000, // Tắm cho cá: lâu hơn
                "Tank Cleaning" => 2000, // Vệ sinh hồ: trung bình
                "Health Check" => 1000,  // Kiểm tra sức khỏe: nhanh nhất
                _ => 1000 // Mặc định nếu có thêm dịch vụ khác
            };
            await Task.Delay(delayTime); // Simulate processing time
            Console.WriteLine($"Service completed: {service.Name}");
        }
    }
}
