using FluentValidation;
using KoiFarmShop.Application.Common.Validator.Appointment;
using KoiFarmShop.Application.Common.Validator.Pet;
using KoiFarmShop.Application.Common.Validator.PetService;
using KoiFarmShop.Application.Common.Validator.User;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using KoiFarmShop.Infrastructure.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using KoiFarmShop.Infrastructure.DTOs.User.Register;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using KVSC.Infrastructure.Implement.Repositories;
using Google.Cloud.Storage.V1;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;

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


            // Tạo IConfiguration
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<KVSCContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MyDb")));
            var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-93ba231cea.json");
            try
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(credentialPath)
                });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as necessary
                throw new Exception("Failed to initialize Firebase.", ex);
            }

            // Register the Google Cloud Storage client and any Firebase related services
            services.AddSingleton(StorageClient.Create(GoogleCredential.FromFile(credentialPath)));
            #region Common
            //Common
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            //Comon
            #endregion

            #region Validator
            //Validator
            services.AddSingleton<IValidator<LoginRequest>, LoginValidator>();
            services.AddSingleton<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddSingleton<IValidator<AddPetRequest>, AddPetValidator>();
            services.AddSingleton<IValidator<AddPetServiceRequest>, AddPetServiceValidator>();
            services.AddSingleton<IValidator<AddComboServiceRequest>, AddComboServiceValidator>();
            services.AddSingleton<IValidator<AddPetServiceCategoryRequest>, AddPetServiceCategoryValidator>();
            services.AddSingleton<IValidator<MakeAppointmentForServiceRequest>, MakeAppointmentForServiceValidator>();
            services.AddSingleton<IValidator<MakeAppointmentForComboRequest>, MakeAppointmentForComboValidator>();

            //Validator
            #endregion

            #region Repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPetRepository, PetRepository>();

            services.AddSingleton<IFirebaseRepository, FirebaseRepository>();

            services.AddSingleton<IPetServiceRepository, PetServiceRepository>();
            services.AddSingleton<IPetServiceCategoryRepository, PetServiceCategoryRepository>();
            services.AddSingleton<IPetServiceCategoryRepository, PetServiceCategoryRepository>();
            services.AddSingleton<IComboServiceRepository, ComboServiceRepository>();
            services.AddSingleton<IAppointmentRepository, AppointmentRepository>();

           
            #endregion


            #region GenericRepositories
            services.AddSingleton<IGenericRepository<User>, GenericRepository<User>>();
            #endregion



            #region Service
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IFirebaseService, FirebaseService>();
            services.AddSingleton<IPetServiceService, PetServiceService>();
            services.AddSingleton<IPetServiceCategoryService, PetServiceCategoryService>();
            services.AddSingleton<IComboServiceService, ComboServiceService>();
            services.AddSingleton<IAppointmentService, AppointmentService>();

            #endregion

            services.AddSingleton<MainWindow>();

            // Tạo ServiceProvider và khởi động MainWindow
            ServiceProvider = services.BuildServiceProvider();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>(); // Chỉ gọi từ DI
            mainWindow.Show();
        }
    }

}
