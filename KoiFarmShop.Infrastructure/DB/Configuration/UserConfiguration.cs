using System.Security.Cryptography;
using System.Text;
using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
            (
                // Admin
                new User
                {
                    Id = new Guid("4feb4940-94dc-4aed-b580-ee116b668704"),
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Admin",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 1,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                // Manager
                new User
                {
                    Id = new Guid("b59d5d37-53d8-4cb6-98ed-520f49eafa73"),
                    FullName = "Manager",
                    Email = "manager@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Manager",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 2
                },
                // Veterinarian_1
                new User
                {
                    Id = new Guid("1dac24c4-08e2-4612-84dc-7c8960e483ea"),
                    FullName = "Veterinarian_1",
                    Email = "veterinarian1@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Veterinarian_1",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 3,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                // Veterinarian_2
                new User
                {
                    Id = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8a"),
                    FullName = "Veterinarian_2",
                    Email = "veterinarian2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Veterinarian_2",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 3,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                
                // Veterinarian_3
                new User
                {
                    Id = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8c"),
                    FullName = "Veterinarian_3",
                    Email = "veterinarian2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Veterinarian_3",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 3,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                //Veterinarian_4
                new User
                {
                    Id = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8d"),
                    FullName = "Veterinarian_4",
                    Email = "veterinarian2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Veterinarian_4",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 3,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                // Staff_1
                new User
                {
                    Id = new Guid("5f28fcb6-675b-4f97-a925-01ac8c68b5ac"),
                    FullName = "Staff_1",
                    Email = "staff1@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Staff_1",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 4
                },
                // Staff_2
                new User
                {
                    Id = new Guid("0d1fbbab-a175-4d90-8291-d5d96ebb9359"),
                    FullName = "Staff_2",
                    Email = "staff2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Staff_2",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 4
                },
                // Customer_1
                new User
                {
                    Id = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"),
                    FullName = "Gia Phuc",
                    Email = "customer1@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Cubin2003",
                    PasswordHash = HashPassword("Cubin2003"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 5,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                // Customer_2
                new User
                {
                    Id = new Guid("bca84e29-de4d-475b-a3ad-a02e937efa14"),
                    FullName = "Customer_2",
                    Email = "customer2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Customer_2",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 5
                },
                // Customer_3
                new User
                {
                    Id = new Guid("45a9dc1c-fb8a-4607-9a7e-d6b1359384d7"),
                    FullName = "Customer_3",
                    Email = "customer3@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Customer_3",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 5
                },
                // Customer_4
                new User
                {
                    Id = new Guid("d13e5c69-8d8a-4b67-b378-0e2de003816b"),
                    FullName = "Customer_4",
                    Email = "customer4@gmail.com",
                    PhoneNumber = "987654321",
                    Address = "456 Elm St",
                    Username = "Customer_4",
                    PasswordHash = HashPassword("String123!"),
                    DateOfBirth = new DateTime(1992, 2, 2),
                    role = 5
                }
            );
        }
    }
}