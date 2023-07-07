using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v11_update_delete_cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Booking",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Booking",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Comment",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBenefit_Hotel",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Room",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Booking",
                table: "Booking",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Booking",
                table: "Booking",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Comment",
                table: "Comments",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBenefit_Hotel",
                table: "Hotel",
                column: "HotelBenefitId",
                principalTable: "HotelBenefit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Room",
                table: "Room",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery",
                column: "RooomId",
                principalTable: "Room",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Booking",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Booking",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Comment",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBenefit_Hotel",
                table: "Hotel");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotel_Room",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Booking",
                table: "Booking",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Booking",
                table: "Booking",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Comment",
                table: "Comments",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBenefit_Hotel",
                table: "Hotel",
                column: "HotelBenefitId",
                principalTable: "HotelBenefit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotel_Room",
                table: "Room",
                column: "HotelId",
                principalTable: "Hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_RoomGallery",
                table: "RoomGallery",
                column: "RooomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
