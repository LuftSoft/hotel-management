using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v3_edit_fk_hotel_hotelbenefit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "HotelBenefit");

            migrationBuilder.AlterColumn<int>(
                name: "HotelBenefitId",
                table: "Hotel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel",
                column: "HotelBenefitId",
                unique: true,
                filter: "[HotelBenefitId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "HotelBenefit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HotelBenefitId",
                table: "Hotel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel",
                column: "HotelBenefitId",
                unique: true);
        }
    }
}
