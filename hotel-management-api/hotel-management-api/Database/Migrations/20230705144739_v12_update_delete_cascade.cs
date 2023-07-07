using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v12_update_delete_cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery",
                column: "RooomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery",
                column: "RooomId",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
