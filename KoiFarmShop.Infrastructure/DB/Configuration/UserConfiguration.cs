using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData
                (
                //1
                new User
                {
                    Id = new Guid("4feb4940-94dc-4aed-b580-ee116b668704"), // Gán Id cố định
                    FullName = "Admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Admin",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 1,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                //2
                new User
                {
                    Id = new Guid("b59d5d37-53d8-4cb6-98ed-520f49eafa73"), // Gán Id cố định
                    FullName = "Manager",
                    Email = "manager@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Manager",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 2
                },
                //3
                new User
                {
                    Id = new Guid("1dac24c4-08e2-4612-84dc-7c8960e483ea"), // Gán Id cố định
                    FullName = "Veterinarian_1",
                    Email = "veterinarian1@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Veterinarian_1",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 3,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                //4
                new User
                {
                    Id = new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8a"), // Gán Id cố định
                    FullName = "Veterinarian_2",
                    Email = "veterinarian2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Veterinarian_2",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 3,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                //5
                new User
                {
                    Id = new Guid("5f28fcb6-675b-4f97-a925-01ac8c68b5ac"), // Gán Id cố định
                    FullName = "Staff_1",
                    Email = "staff1@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Staff_1",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 4
                },
                //6
                new User
                {
                    Id = new Guid("0d1fbbab-a175-4d90-8291-d5d96ebb9359"), // Gán Id cố định
                    FullName = "Staff_2",
                    Email = "staff2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Staff_2",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 4
                },
                //7
                new User
                {
                    Id = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"), // Gán Id cố định
                    FullName = "Customer_1",
                    Email = "customer1@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Customer_1",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 5,
                    ProfilePictureUrl = "User/0c08ff89-61a7-4b57-9c1d-ac6f3c57907b_Screenshot 2024-01-17 155826.png"
                },
                new User
                {
                    Id = new Guid("bca84e29-de4d-475b-a3ad-a02e937efa14"), // Gán Id cố định
                    FullName = "Customer_2",
                    Email = "customer2@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Customer_2",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 5
                },
                new User
                {
                    Id = new Guid("45a9dc1c-fb8a-4607-9a7e-d6b1359384d7"), // Gán Id cố định
                    FullName = "Customer_3",
                    Email = "customer3@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "123 Main St",
                    Username = "Customer_3",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    role = 5
                },
                new User
                {
                    Id = new Guid("d13e5c69-8d8a-4b67-b378-0e2de003816b"),
                    FullName = "Customer_4",
                    Email = "customer4@gmail.com",
                    PhoneNumber = "987654321",
                    Address = "456 Elm St",
                    Username = "Customer_4",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1992, 2, 2),
                    role = 5
                },
                new User
                {
                    Id = new Guid("a1d5c6e2-4f26-4860-9f3e-563a07f1cbf6"),
                    FullName = "Customer_5",
                    Email = "customer5@gmail.com",
                    PhoneNumber = "234567890",
                    Address = "789 Pine St",
                    Username = "Customer_5",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1993, 3, 3),
                    role = 5
                },
                new User
                {
                    Id = new Guid("6b1f16be-c12a-4709-95d4-f0c623239823"),
                    FullName = "Customer_6",
                    Email = "customer6@gmail.com",
                    PhoneNumber = "345678901",
                    Address = "321 Maple St",
                    Username = "Customer_6",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1994, 4, 4),
                    role = 5
                },
                new User
                {
                    Id = new Guid("f1f55d2a-b96f-4c74-b89e-e4c29a1d8e2e"),
                    FullName = "Customer_7",
                    Email = "customer7@gmail.com",
                    PhoneNumber = "456789012",
                    Address = "654 Cedar St",
                    Username = "Customer_7",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1995, 5, 5),
                    role = 5
                },
                new User
                {
                    Id = new Guid("ca28f1ab-1cfc-4e99-835f-e44c65756bb3"),
                    FullName = "Customer_8",
                    Email = "customer8@gmail.com",
                    PhoneNumber = "567890123",
                    Address = "987 Birch St",
                    Username = "Customer_8",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1996, 6, 6),
                    role = 5
                },
                new User
                {
                    Id = new Guid("c30d6f58-b8e0-4fb4-b10c-f9c2af7a3622"),
                    FullName = "Customer_9",
                    Email = "customer9@gmail.com",
                    PhoneNumber = "678901234",
                    Address = "159 Willow St",
                    Username = "Customer_9",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1997, 7, 7),
                    role = 5
                },
                new User
                {
                    Id = new Guid("a41b99c8-7d70-4c8c-9bc3-e249f93c9278"),
                    FullName = "Customer_10",
                    Email = "customer10@gmail.com",
                    PhoneNumber = "789012345",
                    Address = "753 Cherry St",
                    Username = "Customer_10",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1998, 8, 8),
                    role = 5
                },
                new User
                {
                    Id = new Guid("e79cb43f-3b60-4d8d-84c5-579c1d6c80e4"),
                    FullName = "Customer_11",
                    Email = "customer11@gmail.com",
                    PhoneNumber = "890123456",
                    Address = "852 Oak St",
                    Username = "Customer_11",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1999, 9, 9),
                    role = 5
                },
                new User
                {
                    Id = new Guid("f81253a8-7937-4c29-80d3-bcfa0522f9e8"),
                    FullName = "Customer_12",
                    Email = "customer12@gmail.com",
                    PhoneNumber = "901234567",
                    Address = "963 Sycamore St",
                    Username = "Customer_12",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(2000, 10, 10),
                    role = 5
                },
                new User
                {
                    Id = new Guid("4b171e29-8041-4d4d-a96d-4f7efc1f635b"),
                    FullName = "Customer_13",
                    Email = "customer13@gmail.com",
                    PhoneNumber = "012345678",
                    Address = "357 Fir St",
                    Username = "Customer_13",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1988, 11, 11),
                    role = 5
                },
                new User
                {
                    Id = new Guid("c4e31a0b-982b-43e5-bb52-93b7c2e4b307"),
                    FullName = "Customer_14",
                    Email = "customer14@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "258 Spruce St",
                    Username = "Customer_14",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1987, 12, 12),
                    role = 5
                },
                new User
                {
                    Id = new Guid("a255eb98-d5b4-4e57-bbff-1de8c6b844b0"),
                    FullName = "Customer_15",
                    Email = "customer15@gmail.com",
                    PhoneNumber = "987654321",
                    Address = "369 Redwood St",
                    Username = "Customer_15",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1986, 1, 1),
                    role = 5
                },
                new User
                {
                    Id = new Guid("79a27f2e-e21b-49b1-99a3-e66b18c5cba0"),
                    FullName = "Customer_16",
                    Email = "customer16@gmail.com",
                    PhoneNumber = "234567890",
                    Address = "147 Palm St",
                    Username = "Customer_16",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1985, 2, 2),
                    role = 5
                },
                new User
                {
                    Id = new Guid("e3c1e155-dbc4-40a8-8a5a-091557942c55"),
                    FullName = "Customer_17",
                    Email = "customer17@gmail.com",
                    PhoneNumber = "345678901",
                    Address = "258 Acacia St",
                    Username = "Customer_17",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1984, 3, 3),
                    role = 5
                },
                new User
                {
                    Id = new Guid("0f43fda0-fbde-4079-8ae4-66da674c8455"),
                    FullName = "Customer_18",
                    Email = "customer18@gmail.com",
                    PhoneNumber = "456789012",
                    Address = "369 Larch St",
                    Username = "Customer_18",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1983, 4, 4),
                    role = 5
                },
                new User
                {
                    Id = new Guid("fe29d53f-44b8-40b5-8672-bb37f6b5c8c5"),
                    FullName = "Customer_19",
                    Email = "customer19@gmail.com",
                    PhoneNumber = "567890123",
                    Address = "579 Fir St",
                    Username = "Customer_19",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1982, 5, 5),
                    role = 5
                },
                new User
                {
                    Id = new Guid("c5e4f6e5-bd3c-4fd5-b3d7-8a7f9c8e3b44"),
                    FullName = "Customer_20",
                    Email = "customer20@gmail.com",
                    PhoneNumber = "678901234",
                    Address = "690 Cedar St",
                    Username = "Customer_20",
                    PasswordHash = "String123!",
                    DateOfBirth = new DateTime(1981, 6, 6),
                    role = 5
                }
          );
        }
    }
}
