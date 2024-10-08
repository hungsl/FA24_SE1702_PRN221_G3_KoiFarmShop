﻿using KoiFarmShop.Domain.Entities;
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
        public DbSet<PetService> PetServices { get; set; }
        public DbSet<PetServiceCategory> PetServiceCategories { get; set; }
        public DbSet<ComboService> ComboServices { get; set; }
        public DbSet<ComboServiceItem> ComboServiceItems { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<VeterinarianSchedule> VeterinarianSchedules { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<PetHabitat> PetHabitats { get; set; }
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
            modelBuilder.Entity<PetType>().ToTable("PetType");
            modelBuilder.Entity<PetHabitat>().ToTable("PetHabitat");

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

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PetType)
                .WithMany(pt => pt.Pets)
                .HasForeignKey(p => p.PetTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PetType>()
                .HasOne(pt => pt.PetHabitat)
                .WithMany(ph => ph.PetTypes)
                .HasForeignKey(pt => pt.PetHabitatId)
                .OnDelete(DeleteBehavior.Restrict);

            // Call base method
            base.OnModelCreating(modelBuilder);
        }
    }
}
