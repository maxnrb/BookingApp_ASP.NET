using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApp.Migrations
{
    public partial class undo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_Address_AddressId",
                table: "Accommodations");

            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_HouseRules_HouseRulesId",
                table: "Accommodations");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AddressId",
                table: "Accommodations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_HouseRulesId",
                table: "Accommodations",
                column: "HouseRulesId");

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
    }
}
