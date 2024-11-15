using KoiFarmShop.Application.Implement.Service;
using KoiFarmShop.Application.Interface.IService;
using KoiFarmShop.Domain.Entities;
using KoiFarmShop.Infrastructure.DB;
using KoiFarmShop.Infrastructure.Implement.Repositories;
using KoiFarmShop.Infrastructure.Interface.IRepositories;
using KoiFarmShop.RazorWebApp.Startup;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.RazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<KVSCContext>(opt =>
            {
                // Set up your database connection string
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));
            });
            // Register custom services
            builder.Services.RegisterServices();
            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSignalR();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapHub<Hubs.ChatHub>("/chathub");

            app.Run();
        }
    }
}
