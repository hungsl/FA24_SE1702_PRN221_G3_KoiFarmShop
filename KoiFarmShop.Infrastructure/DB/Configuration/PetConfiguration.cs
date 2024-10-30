using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {

            // Seed sample data for Koi fish
            builder.HasData(
                new Pet
                {
                    Id = new Guid("f1111111-1111-1111-1111-111111111111"),
                    Name = "Koi Fish 1",
                    Age = 3,
                    Gender = "Male",
                    ImageUrl = "https://example.com/koi1.jpg",
                    Color = "Orange and White",
                    Length = 35.0,
                    Weight = 2.0,
                    Quantity = 5, // Multiple Koi of the same kind
                    LastHealthCheck = new DateTime(2024, 10, 1),
                    Note = "Healthy with vibrant colors.",
                    HealthStatus = 1, // Healthy 
                    OwnerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"), // Customer_1
                },
                new Pet
                {
                    Id = new Guid("f2222222-2222-2222-2222-222222222222"),
                    Name = "Koi Fish 2",
                    Age = 4,
                    Gender = "Female",
                    ImageUrl = "https://example.com/koi2.jpg",
                    Color = "Red and White",
                    Length = 40.0,
                    Weight = 3.0,
                    Quantity = 3,
                    LastHealthCheck = new DateTime(2024, 10, 2),
                    Note = "Slight issue with fins, under observation.",
                    HealthStatus = 2, // Under Observation
                    OwnerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"), 
                },
                new Pet
                {
                    Id = new Guid("f3333333-3333-3333-3333-333333333333"),
                    Name = "Koi Fish 3",
                    Age = 2,
                    Gender = "Male",
                    ImageUrl = "https://example.com/koi3.jpg",
                    Color = "Yellow and White",
                    Length = 32.0,
                    Weight = 2.0,
                    Quantity = 10, // Large group of the same fish
                    LastHealthCheck = new DateTime(2024, 10, 3),
                    Note = "In great condition, regular feeding.",
                    HealthStatus = 1, // Healthy
                    OwnerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"), 
                },
                new Pet
                {
                    Id = new Guid("f4444444-4444-4444-4444-444444444444"),
                    Name = "Koi Fish 4",
                    Age = 1,
                    Gender = "Female",
                    ImageUrl = "https://example.com/koi4.jpg",
                    Color = "Black and White",
                    Length = 25.0,
                    Weight = 3.0,
                    Quantity = 7,
                    LastHealthCheck = new DateTime(2024, 10, 4),
                    Note = "Newly purchased, adjusting to pond.",
                    HealthStatus = 1, // Healthy
                    OwnerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"), 
                },
                new Pet
                {
                    Id = new Guid("f5555555-5555-5555-5555-555555555555"),
                    Name = "Koi Fish 5",
                    Age = 5,
                    Gender = "Male",
                    ImageUrl = "https://example.com/koi5.jpg",
                    Color = "Blue and White",
                    Length = 45.0,
                    Weight = 3.0,
                    Quantity = 2,
                    LastHealthCheck = new DateTime(2024, 10, 5),
                    Note = "Strong swimmer, excellent condition.",
                    HealthStatus = 1, // Healthy
                    OwnerId = new Guid("0d1fbbab-a175-4d90-8291-d5d96ebb9359"), // Customer_5
                },
                new Pet
                {
                    Id = new Guid("f6666666-6666-6666-6666-666666666666"),
                    Name = "Koi Fish 6",
                    Age = 2,
                    Gender = "Female",
                    ImageUrl = "https://example.com/koi6.jpg",
                    Color = "White",
                    Length = 28.0,
                    Weight = 2.0,
                    Quantity = 4,
                    LastHealthCheck = new DateTime(2024, 10, 6),
                    Note = "Excellent appetite, feeding well.",
                    HealthStatus = 1, // Healthy
                    OwnerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"), // Customer_1
                }
            );
        }
    }
}
