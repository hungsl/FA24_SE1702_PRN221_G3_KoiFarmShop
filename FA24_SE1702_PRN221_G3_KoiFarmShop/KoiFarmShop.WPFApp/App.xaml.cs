using FluentValidation;
using KoiFarmShop.Application.Common.Validator.Appointment;
using KoiFarmShop.Application.Common.Validator.Pet;
using KoiFarmShop.Application.Common.Validator.PetService;
using KoiFarmShop.Application.Common.Validator.User;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using KoiFarmShop.Infrastructure.DTOs.User.Register;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace KoiFarmShop.WPFApp
{
    public partial class App : System.Windows.Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Build Configuration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            // Configure DbContext with Scoped Lifetime
            services.AddDbContext<KVSCContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDb")),
                ServiceLifetime.Scoped);

            // Add Logging
            services.AddLogging(configure => configure.AddConsole())
                .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            #region Common Dependencies
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Change to Scoped
            #endregion

            #region Validators
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddTransient<IValidator<AddPetRequest>, AddPetValidator>();
            services.AddTransient<IValidator<AddPetServiceRequest>, AddPetServiceValidator>();
            services.AddTransient<IValidator<AddComboServiceRequest>, AddComboServiceValidator>();
            services.AddTransient<IValidator<AddPetServiceCategoryRequest>, AddPetServiceCategoryValidator>();
            services.AddTransient<IValidator<MakeAppointmentForServiceRequest>, MakeAppointmentForServiceValidator>();
            services.AddTransient<IValidator<MakeAppointmentForComboRequest>, MakeAppointmentForComboValidator>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IFirebaseRepository, FirebaseRepository>();
            services.AddScoped<IPetServiceRepository, PetServiceRepository>();
            services.AddScoped<IPetServiceCategoryRepository, PetServiceCategoryRepository>();
            services.AddScoped<IComboServiceRepository, ComboServiceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            #endregion

            #region Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IPetServiceService, PetServiceService>();
            services.AddScoped<IPetServiceCategoryService, PetServiceCategoryService>();
            services.AddScoped<IComboServiceService, ComboServiceService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPetServiceLogic, PetServiceLogic>();
            #endregion

            // Register Window with Scoped Lifetime
            services.AddScoped<WindowAppointment>();

            // Build ServiceProvider and start WindowAppointment
            ServiceProvider = services.BuildServiceProvider();
            var windowAppointment = ServiceProvider.GetRequiredService<WindowAppointment>();
            windowAppointment.Show();
        }
    }
}
