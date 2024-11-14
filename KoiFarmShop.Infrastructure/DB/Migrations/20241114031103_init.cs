using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiFarmShop.Infrastructure.DB.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "PetService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_PetService_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "PetService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDimensions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PetServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VeterinarianId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TravelCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_PetService_PetServiceId",
                        column: x => x.PetServiceId,
                        principalTable: "PetService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItem_User_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9727));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9731));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9734));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9737));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9740));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9743));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9749));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9754));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9758));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(73));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f3333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(78));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f4444444-4444-4444-4444-444444444444"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(83));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f5555555-5555-5555-5555-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(87));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f6666666-6666-6666-6666-666666666666"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(91));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("2d547de7-d7a0-4c27-a26c-9cf3a7099817"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(195), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("2d95b900-9b04-4f6f-94ec-7d47d2a89ec8"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(182), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("33e71556-d924-4101-bd1f-8707ca0e6f87"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(190), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("39ebc58b-6731-491d-949d-82f387dce82e"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(186), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("7253ea62-e419-40dc-bc70-e069611587dd"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(203), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("7d80bd0a-7780-4c4c-981b-48d7f8784405"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(178), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("8c0ce681-03e2-4ed8-83b2-abc3db694c5b"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(199), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("c33e3a86-0230-468b-8c06-ee91b7e8cc21"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(206), 0 });

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                columns: new[] { "CreatedDate", "Frequency" },
                values: new object[] { new DateTime(2024, 11, 14, 10, 11, 1, 162, DateTimeKind.Local).AddTicks(173), 0 });

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("15c55a94-06fb-4dac-8b32-7c1d7af085a3"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9967));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("3d3bb172-c3d0-4d0f-ac50-713708bc6498"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("75efc332-0e1b-4d35-a609-4897d83c173e"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9906));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("82b86176-d076-4576-b0f3-60220ca3e5ba"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9918));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("83d70177-2e40-49c9-a0bf-27ce80cce340"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("a5e47a8f-f6e1-4c7a-8955-4a928744f9bf"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("ca3801df-081c-4db5-a416-b04791797d92"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9971));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("da91046c-71d1-429b-ade3-5e8ff9f701a6"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("fb21c5e6-5db5-4dab-99b1-9c5d51f0ab51"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9969));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("fe3df183-1f42-4301-a1fb-35e6211c8816"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 10, 11, 1, 161, DateTimeKind.Local).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0d1fbbab-a175-4d90-8291-d5d96ebb9359"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1dac24c4-08e2-4612-84dc-7c8960e483ea"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9304));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8a"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9321));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8c"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9347));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8d"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9362));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("45a9dc1c-fb8a-4607-9a7e-d6b1359384d7"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9456));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4feb4940-94dc-4aed-b580-ee116b668704"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5f28fcb6-675b-4f97-a925-01ac8c68b5ac"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b59d5d37-53d8-4cb6-98ed-520f49eafa73"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9281));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bca84e29-de4d-475b-a3ad-a02e937efa14"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9440));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d13e5c69-8d8a-4b67-b378-0e2de003816b"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 161, DateTimeKind.Utc).AddTicks(9407));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1b"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(298));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1c"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1d"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(302));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("d59b53f6-7bc4-4af7-b5f5-438e16b75dd4"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 3, 11, 1, 162, DateTimeKind.Utc).AddTicks(294));

            migrationBuilder.CreateIndex(
                name: "IX_Order_CustomerId",
                table: "Order",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_PetId",
                table: "OrderItem",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_PetServiceId",
                table: "OrderItem",
                column: "PetServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_VeterinarianId",
                table: "OrderItem",
                column: "VeterinarianId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_CustomerId",
                table: "Rating",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ServiceId",
                table: "Rating",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "PetService");

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6547));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6554));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6561));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6565));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6570));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6578));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6583));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6590));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6595));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6599));

            migrationBuilder.UpdateData(
                table: "Appointment",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6604));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f1111111-1111-1111-1111-111111111111"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6879));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f2222222-2222-2222-2222-222222222222"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6887));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f3333333-3333-3333-3333-333333333333"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6893));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f4444444-4444-4444-4444-444444444444"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6898));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f5555555-5555-5555-5555-555555555555"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6903));

            migrationBuilder.UpdateData(
                table: "Pet",
                keyColumn: "Id",
                keyValue: new Guid("f6666666-6666-6666-6666-666666666666"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6909));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("2d547de7-d7a0-4c27-a26c-9cf3a7099817"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7090));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("2d95b900-9b04-4f6f-94ec-7d47d2a89ec8"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7072));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("33e71556-d924-4101-bd1f-8707ca0e6f87"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7084));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("39ebc58b-6731-491d-949d-82f387dce82e"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7078));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("7253ea62-e419-40dc-bc70-e069611587dd"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7102));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("7d80bd0a-7780-4c4c-981b-48d7f8784405"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7032));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("8c0ce681-03e2-4ed8-83b2-abc3db694c5b"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7095));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("c33e3a86-0230-468b-8c06-ee91b7e8cc21"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "PetService",
                keyColumn: "Id",
                keyValue: new Guid("f6a59f70-c0db-45b4-a598-045a005d42ed"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(7025));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("15c55a94-06fb-4dac-8b32-7c1d7af085a3"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6762));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("3d3bb172-c3d0-4d0f-ac50-713708bc6498"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6758));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("75efc332-0e1b-4d35-a609-4897d83c173e"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6742));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("82b86176-d076-4576-b0f3-60220ca3e5ba"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("83d70177-2e40-49c9-a0bf-27ce80cce340"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("a5e47a8f-f6e1-4c7a-8955-4a928744f9bf"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("ca3801df-081c-4db5-a416-b04791797d92"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6769));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("da91046c-71d1-429b-ade3-5e8ff9f701a6"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6750));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("fb21c5e6-5db5-4dab-99b1-9c5d51f0ab51"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6766));

            migrationBuilder.UpdateData(
                table: "PetServiceCategory",
                keyColumn: "Id",
                keyValue: new Guid("fe3df183-1f42-4301-a1fb-35e6211c8816"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 20, 15, 21, 907, DateTimeKind.Local).AddTicks(6735));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0d1fbbab-a175-4d90-8291-d5d96ebb9359"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6107));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1dac24c4-08e2-4612-84dc-7c8960e483ea"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(5928));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8a"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6009));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8c"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6036));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("2430f703-cb67-4225-bb7e-c9abe5803b8d"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6060));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("45a9dc1c-fb8a-4607-9a7e-d6b1359384d7"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6183));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4feb4940-94dc-4aed-b580-ee116b668704"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(5574));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("5f28fcb6-675b-4f97-a925-01ac8c68b5ac"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6083));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b59d5d37-53d8-4cb6-98ed-520f49eafa73"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(5880));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("bca84e29-de4d-475b-a3ad-a02e937efa14"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("d13e5c69-8d8a-4b67-b378-0e2de003816b"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6206));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("dd0e9f37-d587-401d-932e-7f098eb60b3e"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1b"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(7220));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1c"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("21a15a4f-32f5-4d45-a056-f0d61f384e1d"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(7228));

            migrationBuilder.UpdateData(
                table: "Veterinarian",
                keyColumn: "Id",
                keyValue: new Guid("d59b53f6-7bc4-4af7-b5f5-438e16b75dd4"),
                column: "CreatedDate",
                value: new DateTime(2024, 10, 30, 13, 15, 21, 907, DateTimeKind.Utc).AddTicks(7213));
        }
    }
}
