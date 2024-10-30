using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class PetServiceCategoryConfiguration : IEntityTypeConfiguration<PetServiceCategory>
    {
        public void Configure(EntityTypeBuilder<PetServiceCategory> builder)
        {
            builder.HasData
            (
                new PetServiceCategory
                {
                    Id = new Guid("83d70177-2e40-49c9-a0bf-27ce80cce340"),
                    Name = "Health Check",
                    Description = "A standard health check for Koi fish to monitor their overall well-being and prevent diseases.",
                    ServiceType = "Health",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("fe3df183-1f42-4301-a1fb-35e6211c8816"),
                    Name = "Feeding Service",
                    Description = "Specialized feeding service for Koi fish, ensuring proper nutrition and dietary requirements.",
                    ServiceType = "Feeding",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("75efc332-0e1b-4d35-a609-4897d83c173e"),
                    Name = "Water Quality Testing",
                    Description = "Testing water quality parameters to ensure a healthy environment for Koi.",
                    ServiceType = "Testing",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("a5e47a8f-f6e1-4c7a-8955-4a928744f9bf"),
                    Name = "Fungal Treatment",
                    Description = "Treatment services for Koi suffering from fungal infections.",
                    ServiceType = "Treatment",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("da91046c-71d1-429b-ade3-5e8ff9f701a6"),
                    Name = "Parasite Treatment",
                    Description = "Services to treat and prevent parasites in Koi fish.",
                    ServiceType = "Treatment",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("82b86176-d076-4576-b0f3-60220ca3e5ba"),
                    Name = "Pond Maintenance",
                    Description = "Regular maintenance services for Koi ponds to ensure optimal conditions.",
                    ServiceType = "Maintenance",
                    ApplicableTo = "Ponds",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("3d3bb172-c3d0-4d0f-ac50-713708bc6498"),
                    Name = "Koi Breeding Assistance",
                    Description = "Guidance and assistance in breeding Koi fish.",
                    ServiceType = "Breeding",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("15c55a94-06fb-4dac-8b32-7c1d7af085a3"),
                    Name = "Koi Transportation",
                    Description = "Safe transportation services for Koi fish.",
                    ServiceType = "Transportation",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("fb21c5e6-5db5-4dab-99b1-9c5d51f0ab51"),
                    Name = "Emergency Care",
                    Description = "Emergency medical services for Koi in distress.",
                    ServiceType = "Emergency",
                    ApplicableTo = "Koi Fish",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new PetServiceCategory
                {
                    Id = new Guid("ca3801df-081c-4db5-a416-b04791797d92"),
                    Name = "Educational Workshops",
                    Description = "Workshops on Koi care and pond management.",
                    ServiceType = "Education",
                    ApplicableTo = "Koi Enthusiasts",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                }
            );
        }
    }
}
