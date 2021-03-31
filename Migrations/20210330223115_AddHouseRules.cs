using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApp.Migrations
{
    public partial class AddHouseRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HouseRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArrivalHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    DepartureHour = table.Column<TimeSpan>(type: "time", nullable: false),
                    PetAllowed = table.Column<bool>(type: "bit", nullable: false),
                    PartyAllowed = table.Column<bool>(type: "bit", nullable: false),
                    SmokeAllowed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseRules_Accommodations_Id",
                        column: x => x.Id,
                        principalTable: "Accommodations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseRules");
        }
    }
}
