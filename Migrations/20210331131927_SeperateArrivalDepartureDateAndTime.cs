using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApp.Migrations
{
    public partial class SeperateArrivalDepartureDateAndTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureDateTime",
                table: "Booking",
                newName: "DepartureDate");

            migrationBuilder.RenameColumn(
                name: "ArrivalDateTime",
                table: "Booking",
                newName: "ArrivalDate");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ArrivalTime",
                table: "Booking",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DepartureTime",
                table: "Booking",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Booking",
                newName: "DepartureDateTime");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Booking",
                newName: "ArrivalDateTime");
        }
    }
}
