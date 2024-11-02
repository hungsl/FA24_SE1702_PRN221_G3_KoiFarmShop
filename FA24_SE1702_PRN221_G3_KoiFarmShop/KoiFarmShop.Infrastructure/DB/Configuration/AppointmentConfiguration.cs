using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KVSC.Infrastructure.DB.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {

            // Seed sample data for appointments in each status (Pending, Waiting, InProgress, Reported, Completed)
            builder.HasData(
                // Pending Appointments
                new Appointment
                {
                    Id = new Guid("11111111-1111-1111-1111-111111111111"),
                    CustomerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"),
                    PetId = new Guid("f1111111-1111-1111-1111-111111111111"),
                    PetServiceId = new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                    AppointmentDate = new DateTime(2024, 11, 1, 10, 0, 0), // Friday
                    Status = "Pending",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = new Guid("22222222-2222-2222-2222-222222222222"),
                    CustomerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"),
                    PetId = new Guid("f1111111-1111-1111-1111-111111111111"),
                    PetServiceId = new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                    AppointmentDate = new DateTime(2024, 11, 1, 10, 0, 0), // Friday
                    Status = "Waiting",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = new Guid("33333333-3333-3333-3333-333333333333"),
                    CustomerId = new Guid("45a9dc1c-fb8a-4607-9a7e-d6b1359384d7"),
                    PetId = new Guid("f3333333-3333-3333-3333-333333333333"),
                    PetServiceId = new Guid("2d95b900-9b04-4f6f-94ec-7d47d2a89ec8"),
                    AppointmentDate = new DateTime(2024, 11, 2, 9, 0, 0), // Saturday
                    Status = "Pending",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },

                // Waiting Appointments
                new Appointment
                {
                    Id = new Guid("44444444-4444-4444-4444-444444444444"),
                    CustomerId = new Guid("bca84e29-de4d-475b-a3ad-a02e937efa14"),
                    PetServiceId = new Guid("7d80bd0a-7780-4c4c-981b-48d7f8784405"),
                    AppointmentDate = new DateTime(2024, 11, 3, 13, 30, 0), // Sunday
                    Status = "Waiting",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = new Guid("55555555-5555-5555-5555-555555555555"),
                    CustomerId = new Guid("b59d5d37-53d8-4cb6-98ed-520f49eafa73"),
                    PetServiceId = new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                    AppointmentDate = new DateTime(2024, 11, 5, 14, 0, 0), // Tuesday
                    Status = "Waiting",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },

                // InProgress Appointments
                new Appointment
                {
                    Id = new Guid("66666666-6666-6666-6666-666666666666"),
                    CustomerId = new Guid("5f28fcb6-675b-4f97-a925-01ac8c68b5ac"),
                    PetServiceId = new Guid("7d80bd0a-7780-4c4c-981b-48d7f8784405"),
                    AppointmentDate = new DateTime(2024, 11, 6, 10, 0, 0), // Wednesday
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = new Guid("77777777-7777-7777-7777-777777777777"),
                    CustomerId = new Guid("0d1fbbab-a175-4d90-8291-d5d96ebb9359"),
                    PetServiceId = new Guid("2d95b900-9b04-4f6f-94ec-7d47d2a89ec8"),
                    AppointmentDate = new DateTime(2024, 11, 8, 12, 0, 0), // Friday
                    Status = "InProgress",
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },

                // Reported Appointments
                new Appointment
                {
                    Id = new Guid("88888888-8888-8888-8888-888888888888"),
                    CustomerId = new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"),
                    PetServiceId = new Guid("7d80bd0a-7780-4c4c-981b-48d7f8784405"),
                    AppointmentDate = new DateTime(2024, 11, 10, 14, 0, 0), // Sunday
                    Status = "Reported",
                    CompletedDate = new DateTime(2024, 11, 11),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = new Guid("99999999-9999-9999-9999-999999999999"),
                    CustomerId = new Guid("45a9dc1c-fb8a-4607-9a7e-d6b1359384d7"),
                    PetServiceId = new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                    AppointmentDate = new DateTime(2024, 11, 12, 9, 0, 0), // Tuesday
                    Status = "Reported",
                    CompletedDate = new DateTime(2024, 11, 13),
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },

                // Completed Appointments
                new Appointment
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    CustomerId = new Guid("bca84e29-de4d-475b-a3ad-a02e937efa14"),
                    PetServiceId = new Guid("2d95b900-9b04-4f6f-94ec-7d47d2a89ec8"),
                    AppointmentDate = new DateTime(2024, 11, 7, 11, 0, 0), // Thursday
                    Status = "Completed",
                    CompletedDate = new DateTime(2024, 11, 7, 16, 0, 0), // Completed on the same day
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Appointment
                {
                    Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    CustomerId = new Guid("b59d5d37-53d8-4cb6-98ed-520f49eafa73"),
                    PetServiceId = new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                    AppointmentDate = new DateTime(2024, 11, 9, 10, 0, 0), // Saturday
                    Status = "Completed",
                    CompletedDate = new DateTime(2024, 11, 9, 14, 0, 0), // Completed on the same day
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            );
        }
    }
}
