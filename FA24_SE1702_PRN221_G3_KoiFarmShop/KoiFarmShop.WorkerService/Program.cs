using FluentValidation;
using KoiFarmShop.Application.Common.Validator.Appointment;
using KoiFarmShop.Application.Common.Validator.Pet;
using KoiFarmShop.Application.Common.Validator.PetService;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoiFarmShop.WorkerService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddDbContext<KVSCContext>(options =>
                   options.UseSqlServer(hostContext.Configuration.GetConnectionString("MyDb")));

               services.AddScoped<TaskService>();
               #region Common
               //Common
               services.AddTransient<IPasswordHasher, PasswordHasher>();
               services.AddTransient<UnitOfWork>();
               services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
               services.AddTransient<IUnitOfWork, UnitOfWork>();
               //Comon
               #endregion

               #region Validator
               //Validator
               services.AddTransient<IValidator<AddPetRequest>, AddPetValidator>();
               services.AddTransient<IValidator<AddPetServiceRequest>, AddPetServiceValidator>();
               services.AddTransient<IValidator<AddComboServiceRequest>, AddComboServiceValidator>();
               services.AddTransient<IValidator<AddPetServiceCategoryRequest>, AddPetServiceCategoryValidator>();
               services.AddTransient<IValidator<MakeAppointmentForServiceRequest>, MakeAppointmentForServiceValidator>();
               services.AddTransient<IValidator<MakeAppointmentForComboRequest>, MakeAppointmentForComboValidator>();

               //Validator
               #endregion

               #region Repositories
               services.AddTransient<IUserRepository, UserRepository>();
               services.AddTransient<IPetRepository, PetRepository>();

               services.AddTransient<IFirebaseRepository, FirebaseRepository>();

               services.AddTransient<IPetServiceRepository, PetServiceRepository>();
               services.AddTransient<IPetServiceCategoryRepository, PetServiceCategoryRepository>();
               services.AddTransient<IPetServiceCategoryRepository, PetServiceCategoryRepository>();
               services.AddTransient<IComboServiceRepository, ComboServiceRepository>();
               services.AddTransient<IAppointmentRepository, AppointmentRepository>();


               #endregion


               #region GenericRepositories
               services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
               #endregion



               #region Service
               services.AddTransient<IAuthService, AuthService>();
               services.AddTransient<IFirebaseService, FirebaseService>();
               services.AddTransient<IPetServiceService, PetServiceService>();
               services.AddTransient<IPetServiceCategoryService, PetServiceCategoryService>();
               services.AddTransient<IComboServiceService, ComboServiceService>();
               services.AddTransient<IAppointmentService, AppointmentService>();
               services.AddTransient<IPetServiceLogic, PetServiceLogic>();

               #endregion



               services.AddHostedService<Worker>();
           });
    }
}
