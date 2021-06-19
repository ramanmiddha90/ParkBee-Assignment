using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkBee.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(nullable: false),
                    StreetAddress = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ZoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "GarageDoorStatusHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DoorId = table.Column<Guid>(nullable: false),
                    currentStatus = table.Column<bool>(nullable: false),
                    LastStatus = table.Column<bool>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarageDoorStatusHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garages",
                columns: table => new
                {
                    GarageId = table.Column<Guid>(nullable: false),
                    GarageName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Latitide = table.Column<int>(nullable: false),
                    Longitude = table.Column<int>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    HasBarrier = table.Column<bool>(nullable: false),
                    IsSuspended = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garages", x => x.GarageId);
                    table.ForeignKey(
                        name: "FK_Garages_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doors",
                columns: table => new
                {
                    DoorId = table.Column<Guid>(nullable: false),
                    DoorType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IPAddress = table.Column<string>(nullable: true),
                    GarageDetailGarageId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doors", x => x.DoorId);
                    table.ForeignKey(
                        name: "FK_Doors_Garages_GarageDetailGarageId",
                        column: x => x.GarageDetailGarageId,
                        principalTable: "Garages",
                        principalColumn: "GarageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doors_GarageDetailGarageId",
                table: "Doors",
                column: "GarageDetailGarageId");

            migrationBuilder.CreateIndex(
                name: "IX_Garages_AddressId",
                table: "Garages",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doors");

            migrationBuilder.DropTable(
                name: "GarageDoorStatusHistory");

            migrationBuilder.DropTable(
                name: "Garages");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
