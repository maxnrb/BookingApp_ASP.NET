using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApp.Migrations
{
    public partial class addAmenity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Accommodations_Id",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_HouseRules_Accommodations_Id",
                table: "HouseRules");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressId",
                table: "Accommodations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseRulesId",
                table: "Accommodations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amenity_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_HouseRulesId",
                table: "Accommodations",
                column: "HouseRulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenity_RoomId",
                table: "Amenity",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Address_AddressId",
                table: "Accommodations",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_HouseRules_HouseRulesId",
                table: "Accommodations",
                column: "HouseRulesId",
                principalTable: "HouseRules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Address_AddressId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_HouseRules_HouseRulesId",
                table: "Accommodations");

            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_HouseRulesId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "HouseRulesId",
                table: "Accommodations");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Accommodations_Id",
                table: "Address",
                column: "Id",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseRules_Accommodations_Id",
                table: "HouseRules",
                column: "Id",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
