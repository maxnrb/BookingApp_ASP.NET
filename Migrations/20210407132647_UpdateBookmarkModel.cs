using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApp.Migrations
{
    public partial class UpdateBookmarkModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Accommodations_AccommodationId",
                table: "Bookmark");

            migrationBuilder.RenameColumn(
                name: "AccommodationId",
                table: "Bookmark",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_AccommodationId",
                table: "Bookmark",
                newName: "IX_Bookmark_OfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Offers_OfferId",
                table: "Bookmark",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmark_Offers_OfferId",
                table: "Bookmark");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Bookmark",
                newName: "AccommodationId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookmark_OfferId",
                table: "Bookmark",
                newName: "IX_Bookmark_AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmark_Accommodations_AccommodationId",
                table: "Bookmark",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
