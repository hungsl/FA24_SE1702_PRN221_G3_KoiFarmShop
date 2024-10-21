using KoiFarmShop.Domain.Entities;
using KVSC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Infrastructure.DB
{
    public class KVSCContext : DbContext
    {
        public KVSCContext() { }
        public KVSCContext(DbContextOptions<KVSCContext> options)
            : base(options)
        {
        }


        #region DBSet
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Payment> Payments { get; set; }    
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PetService> PetServices { get; set; }
        public DbSet<PetServiceCategory> PetServiceCategories { get; set; }
        public DbSet<ComboService> ComboServices { get; set; }
        public DbSet<ComboServiceItem> ComboServiceItems { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<VeterinarianSchedule> VeterinarianSchedules { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define table names
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Pet>().ToTable("Pet");
            modelBuilder.Entity<Payment>().ToTable("Payment");
            modelBuilder.Entity<PetService>().ToTable("PetService");
            modelBuilder.Entity<PetServiceCategory>().ToTable("PetServiceCategory");
            modelBuilder.Entity<ComboService>().ToTable("ComboService");
            modelBuilder.Entity<ComboServiceItem>().ToTable("ComboServiceItem");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Veterinarian>().ToTable("Veterinarian");
            modelBuilder.Entity<VeterinarianSchedule>().ToTable("VeterinarianSchedule");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItem");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<CartItem>().ToTable("CartItem");
            // User has many Pets
            modelBuilder.Entity<User>()
                .HasMany(u => u.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId);

         
            // PetServiceCategory has many PetServices
            modelBuilder.Entity<PetServiceCategory>()
                .HasMany(psc => psc.PetServices)
                .WithOne(ps => ps.PetServiceCategory)
                .HasForeignKey(ps => ps.PetServiceCategoryId);

            // ComboService -> ComboServiceItem (1-to-many)
            modelBuilder.Entity<ComboService>()
                .HasMany(c => c.ComboServiceItems)
                .WithOne(csi => csi.ComboService)
                .HasForeignKey(csi => csi.ComboServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // PetService -> ComboServiceItem (1-to-many)
            modelBuilder.Entity<PetService>()
                .HasMany(ps => ps.ComboServiceItems)
                .WithOne(csi => csi.PetService)
                .HasForeignKey(csi => csi.PetServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointment relationships
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Customer)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.CustomerId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PetId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.PetService)
                .WithMany()
                .HasForeignKey(a => a.PetServiceId);

            modelBuilder.Entity<Appointment>()
               .HasOne(a => a.ComboService)
               .WithMany()
               .HasForeignKey(a => a.ComboServiceId);

            modelBuilder.Entity<Appointment>()
               .HasMany(av => av.AppointmentVeterinarians)
               .WithOne(a => a.Appointment)
               .HasForeignKey(av => av.AppointmentId)
               .OnDelete(DeleteBehavior.Restrict);

            // User has many Orders
            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            // User has many Carts
            modelBuilder.Entity<User>()
                .HasMany(u => u.Carts)
                .WithOne(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);

            // Cart has many CartItems
            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            // Order has many OrderItems
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            // OrderItem relationships (optional foreign keys)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Pet)
                .WithMany()
                .HasForeignKey(oi => oi.PetId)
                .OnDelete(DeleteBehavior.Restrict); // Optional, can change based on your needs

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.PetService)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(oi => oi.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Veterinarian)
                .WithMany()
                .HasForeignKey(oi => oi.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict);

            // PetService has many OrderItems
            modelBuilder.Entity<PetService>()
                .HasMany(ps => ps.OrderItems)
                .WithOne(oi => oi.PetService)
                .HasForeignKey(oi => oi.ServiceId);

            // Product has many OrderItems
            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);

            // ProductCategory has many Products
            modelBuilder.Entity<ProductCategory>()
                    .HasMany(pc => pc.Products)
                    .WithOne(p => p.ProductCategory)
                    .HasForeignKey(p => p.ProductCategoryId)
                    .OnDelete(DeleteBehavior.Cascade); // Adjust behavior based on your requirements


            modelBuilder.Entity<AppointmentVeterinarian>()
                .HasOne(av => av.Veterinarian)
                .WithMany(v => v.AppointmentVeterinarians)
                .HasForeignKey(av => av.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ một-một giữa User và Veterinarian
            modelBuilder.Entity<Veterinarian>()
                .HasOne(v => v.User)
                .WithOne(u => u.Veterinarian)
                .HasForeignKey<Veterinarian>(v => v.UserId);

            // Veterinarian has many schedules
            modelBuilder.Entity<VeterinarianSchedule>()
                .HasOne(vs => vs.Veterinarian)
                .WithMany(v => v.VeterinarianSchedules)
                .HasForeignKey(vs => vs.VeterinarianId);

            // Call base method
            base.OnModelCreating(modelBuilder);
        }
    }
}
