using FluentValidation;
using KoiFarmShop.Application.Common.Validator.Appointment;
using KoiFarmShop.Application.Common.Validator.Pet;
using KoiFarmShop.Application.Common.Validator.PetService;
using KoiFarmShop.Application.Common.Validator.User;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
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
using KoiFarmShop.Service.Implement.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace KoiFarmShop.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Set up IConfiguration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            // Register DbContext with a scoped lifetime
            services.AddDbContext<KVSCContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDb")));

            // Register logging
            services.AddLogging(configure => configure.AddConsole());

            #region Common Services
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            // Register UnitOfWork as both IUnitOfWork and its concrete type
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<UnitOfWork>(); // Register the concrete type separately
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region Validators
            services.AddSingleton<IValidator<LoginRequest>, LoginValidator>();
            services.AddSingleton<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddSingleton<IValidator<AddPetRequest>, AddPetValidator>();
            services.AddSingleton<IValidator<AddPetServiceRequest>, AddPetServiceValidator>();
            services.AddSingleton<IValidator<AddComboServiceRequest>, AddComboServiceValidator>();
            services.AddSingleton<IValidator<AddPetServiceCategoryRequest>, AddPetServiceCategoryValidator>();
            services.AddSingleton<IValidator<MakeAppointmentForServiceRequest>, MakeAppointmentForServiceValidator>();
            services.AddSingleton<IValidator<MakeAppointmentForComboRequest>, MakeAppointmentForComboValidator>();
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

            #region Generic Repositories
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            #endregion

            #region Application Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IPetServiceService, PetServiceService>();
            services.AddScoped<IPetServiceCategoryService, PetServiceCategoryService>();
            services.AddScoped<IComboServiceService, ComboServiceService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            #endregion

            // Register MainWindow with DI
            services.AddSingleton<MainWindow>();

            // Build the service provider
            ServiceProvider = services.BuildServiceProvider();

            // Start MainWindow
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
