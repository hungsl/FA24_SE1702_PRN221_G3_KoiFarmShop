using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class VeterinarianConfiguration : IEntityTypeConfiguration<Veterinarian>
    {
        public void Configure(EntityTypeBuilder<Veterinarian> builder)
        {
            // Seed sample data
            builder.HasData(
                new Veterinarian
                {
                    Id = new Guid("d59b53f6-7bc4-4af7-b5f5-438e16b75dd4"),
                    UserId = new Guid("1dac24c4-08e2-4612-84dc-7c8960e483ea"),
                    LicenseNumber = "VN123456",
                    Specialty = "Aquatic Veterinary Medicine",
                    Qualifications = "Doctor of Veterinary Medicine (DVM)",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Veterinarian
                {
                    Id = new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1b"),
                    UserId = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8a"),
                    LicenseNumber = "VN789012",
                    Specialty = "Fish Surgery",
                    Qualifications = "PhD in Veterinary Science",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Veterinarian
                {
                    Id = new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1c"),
                    UserId = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8c"),
                    LicenseNumber = "VN789013",
                    Specialty = "Fish Surgery",
                    Qualifications = "PhD in Veterinary Science",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Veterinarian
                {
                    Id = new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1d"),
                    UserId = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8d"),
                    LicenseNumber = "VN789014",
                    Specialty = "Fish Surgery",
                    Qualifications = "PhD in Veterinary Science",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
               
                
            );
        }
    }
}
