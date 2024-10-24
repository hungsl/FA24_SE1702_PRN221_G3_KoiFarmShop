using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Async
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Đăng ký IPetServiceService trong DI container
            services.AddScoped<IPetServiceService, PetServiceService>();

            // Cấu hình DbContext và kết nối đến SQL Server
            services.AddDbContext<KVSCContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDb")));
        }
    }
}
