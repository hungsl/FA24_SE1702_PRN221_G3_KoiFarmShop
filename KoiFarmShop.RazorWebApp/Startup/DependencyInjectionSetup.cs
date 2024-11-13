using FirebaseAdmin;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using KoiFarmShop.Application.Common.Validator.Pet;

using KoiFarmShop.Application.Common.Validator.Appointment;
using KoiFarmShop.Application.Common.Validator.PetService;

using KoiFarmShop.Application.Common.Validator.User;
using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Application.Common.Validator.Abstract;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.Common;
using KoiFarmShop.Infrastructure.DTOs.Appointment.MakeAppointment;
using KoiFarmShop.Infrastructure.DTOs.Pet.AddPet;



using KoiFarmShop.Infrastructure.DTOs.User.Register;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KoiFarmShop.Infrastructure.Interface;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using KoiFarmShop.Infrastructure.DTOs.User.Login;
using Microsoft.AspNetCore.Identity;
using KoiFarmShop.Infrastructure.DTOs.PetService.AddPetService;
using KoiFarmShop.Infrastructure.DTOs.ComboService.AddComboService;
using KVSC.Infrastructure.Interface.IRepositories;
using KVSC.Infrastructure.Implement.Repositories;
using KVSC.Application.Interface.IService;
using KVSC.Application.Implement.Service;
using KVSC.Infrastructure.DTOs.Rating.AddRating;
using KVSC.Application.Common.Validator.Abstract;
using KVSC.Application.Common.Validator.Rating;
using KVSC.Infrastructure.DTOs.Rating.UpdateRating;
namespace KoiFarmShop.RazorWebApp.Startup
{
    public static class DependencyInjectionSetup
    {

        //comr
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

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
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddTransient<IValidator<AddPetRequest>, AddPetValidator>();
            services.AddTransient<IValidator<AddPetServiceRequest>, AddPetServiceValidator>();
            services.AddTransient<IValidator<AddComboServiceRequest>, AddComboServiceValidator>();
            services.AddTransient<IValidator<AddPetServiceCategoryRequest>, AddPetServiceCategoryValidator>();
            services.AddTransient<IValidator<MakeAppointmentForServiceRequest>, MakeAppointmentForServiceValidator>();
            services.AddTransient<IValidator<MakeAppointmentForComboRequest>, MakeAppointmentForComboValidator>();
            services.AddTransient<IValidator<AddRatingRequest>, AddRatingValidator>();
            services.AddTransient<IValidator<UpdateRatingRequest>, UpdateRatingValidator>();

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
            services.AddTransient<IRatingRepository, RatingRepository>();


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
            services.AddTransient<IRatingService, RatingService>();
            #endregion





            return services;
        }
    }
}
