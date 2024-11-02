using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Async.ServiceApp;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.RazorWebApp.Startup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoiFarmShop.Async
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var appointmentService = services.GetRequiredService<IAppointmentService>();
                var app = new AppointmentServiceApp(appointmentService);

                await app.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            services.RegisterServices(); // Ensure it includes IAppointmentService registration

            services.AddDbContext<KVSCContext>(options =>
                options.UseSqlServer(context.Configuration.GetConnectionString("MyDb")));
        });

    }
}
