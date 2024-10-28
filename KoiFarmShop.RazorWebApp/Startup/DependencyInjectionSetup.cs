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
using KVSC.Infrastructure.Implement.Repositories;
using KVSC.Application.Common.Validator.Product;
using KVSC.Infrastructure.DTOs.Product.AddProduct;
using KVSC.Infrastructure.DTOs.Product.UpdateProduct;
using KVSC.Infrastructure.Interface.IRepositories;
using KVSC.Application.Implement.Service;
using KVSC.Application.Interface.IService;
using KVSC.Application.Common.Validator.ProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.AddProductCategory;
using KVSC.Infrastructure.DTOs.ProductCategory.UpdateProductCategory;
namespace KoiFarmShop.RazorWebApp.Startup
{
    public static class DependencyInjectionSetup
    {

        //comr
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {


            var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-0a84673789.json");
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
            services.AddTransient<IValidator<AddProductRequest>, AddProductValidator>();
            services.AddTransient<IValidator<UpdateProductRequest>, UpdateProductValidator>();
            services.AddTransient<IValidator<AddProductCategoryRequest>, AddProductCategoryValidator>();
            services.AddTransient<IValidator<UpdateProductCategoryRequest>, UpdateProductCategoryValidator>();

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
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();


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
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();

            #endregion





            return services;
        }
    }
}
