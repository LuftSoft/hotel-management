using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v9_update_booking_refreshtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_BookingId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "IsReturned",
                table: "Booking",
                newName: "Returned");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookingId",
                table: "Comments",
                column: "BookingId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_BookingId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Returned",
                table: "Booking",
                newName: "IsReturned");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_BookingId",
                table: "Comments",
                column: "BookingId");
        }
    }
}
