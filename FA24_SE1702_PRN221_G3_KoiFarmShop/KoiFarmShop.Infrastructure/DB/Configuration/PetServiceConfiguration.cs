using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class PetServiceConfiguration : IEntityTypeConfiguration<PetService>
    {
        public void Configure(EntityTypeBuilder<PetService> builder)
        {
            builder.HasData
            (
                new PetService
                {
                    Id = new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                    PetServiceCategoryId = new Guid("3d3bb172-c3d0-4d0f-ac50-713708bc6498"),
                    BasePrice = 150.00m,
                    Duration = "3 hours",
                    ImageUrl = "https://example.com/image7.jpg",
                    AvailableFrom = new DateTime(2024, 10, 01, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 31, 20, 00, 00),
                    TravelCost = 30.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Emergency Care",
                    Description = "Provides immediate care for critical conditions in Koi fish."
                },
                new PetService
                {
                    Id = new Guid("7d80bd0a-7780-4c4c-981b-48d7f8784405"),
                    PetServiceCategoryId = new Guid("da91046c-71d1-429b-ade3-5e8ff9f701a6"),
                    BasePrice = 100.00m,
                    Duration = "2 hours",
                    ImageUrl = "https://example.com/image5.jpg",
                    AvailableFrom = new DateTime(2024, 10, 01, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 31, 20, 00, 00),
                    TravelCost = 25.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Parasite Treatment",
                    Description = "A specialized treatment to remove and prevent parasites in Koi."
                },
                new PetService
                {
                    Id = new Guid("2d95b900-9b04-4f6f-94ec-7d47d2a89ec8"),
                    PetServiceCategoryId = new Guid("75efc332-0e1b-4d35-a609-4897d83c173e"),
                    BasePrice = 20.00m,
                    Duration = "45 minutes",
                    ImageUrl = "https://example.com/image3.jpg",
                    AvailableFrom = new DateTime(2024, 10, 03, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 25, 20, 00, 00),
                    TravelCost = 5.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Water Quality Testing",
                    Description = "Ensures optimal water conditions for Koi health and growth."
                },
                new PetService
                {
                    Id = new Guid("39ebc58b-6731-491d-949d-82f387dce82e"),
                    PetServiceCategoryId = new Guid("a5e47a8f-f6e1-4c7a-8955-4a928744f9bf"),
                    BasePrice = 29.99m,
                    Duration = "30 minutes",
                    ImageUrl = "https://example.com/image.jpg",
                    AvailableFrom = new DateTime(2024, 10, 03, 22, 10, 20),
                    AvailableTo = new DateTime(2024, 10, 04, 02, 10, 20),
                    TravelCost = 10.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Koi Feeding Service",
                    Description = "A convenient feeding service ensuring proper diet and nutrition."
                },
                new PetService
                {
                    Id = new Guid("33e71556-d924-4101-bd1f-8707ca0e6f87"),
                    PetServiceCategoryId = new Guid("fe3df183-1f42-4301-a1fb-35e6211c8816"),
                    BasePrice = 30.00m,
                    Duration = "1 hour",
                    ImageUrl = "https://example.com/image2.jpg",
                    AvailableFrom = new DateTime(2024, 10, 05, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 20, 20, 00, 00),
                    TravelCost = 15.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Koi Feeding Service",
                    Description = "Scheduled feeding for Koi with premium quality fish food."
                },
                new PetService
                {
                    Id = new Guid("2d547de7-d7a0-4c27-a26c-9cf3a7099817"),
                    PetServiceCategoryId = new Guid("a5e47a8f-f6e1-4c7a-8955-4a928744f9bf"),
                    BasePrice = 75.00m,
                    Duration = "1.5 hours",
                    ImageUrl = "https://example.com/image4.jpg",
                    AvailableFrom = new DateTime(2024, 10, 10, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 15, 20, 00, 00),
                    TravelCost = 20.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Fungal Treatment",
                    Description = "Treats and prevents fungal infections in Koi fish."
                },
                new PetService
                {
                    Id = new Guid("8c0ce681-03e2-4ed8-83b2-abc3db694c5b"),
                    PetServiceCategoryId = new Guid("15c55a94-06fb-4dac-8b32-7c1d7af085a3"),
                    BasePrice = 40.00m,
                    Duration = "1 hour",
                    ImageUrl = "https://example.com/image8.jpg",
                    AvailableFrom = new DateTime(2024, 10, 03, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 28, 20, 00, 00),
                    TravelCost = 12.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Educational Workshops",
                    Description = "Provides essential knowledge for Koi fish care and maintenance."
                },
                new PetService
                {
                    Id = new Guid("7253ea62-e419-40dc-bc70-e069611587dd"),
                    PetServiceCategoryId = new Guid("83d70177-2e40-49c9-a0bf-27ce80cce340"),
                    BasePrice = 1.00m,
                    Duration = "string",
                    ImageUrl = "string",
                    AvailableFrom = new DateTime(2024, 10, 04, 14, 02, 32),
                    AvailableTo = new DateTime(2024, 11, 03, 14, 02, 32),
                    TravelCost = 0.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "string",
                    Description = "Sample service description."
                },
                new PetService
                {
                    Id = new Guid("c33e3a86-0230-468b-8c06-ee91b7e8cc21"),
                    PetServiceCategoryId = new Guid("82b86176-d076-4576-b0f3-60220ca3e5ba"),
                    BasePrice = 60.00m,
                    Duration = "1 hour",
                    ImageUrl = "https://example.com/image6.jpg",
                    AvailableFrom = new DateTime(2024, 10, 05, 08, 00, 00),
                    AvailableTo = new DateTime(2024, 10, 30, 20, 00, 00),
                    TravelCost = 15.00m,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    Name = "Pond Maintenance",
                    Description = "Regular maintenance to keep your Koi pond in optimal condition."
                }
            );
        }

    }
}
