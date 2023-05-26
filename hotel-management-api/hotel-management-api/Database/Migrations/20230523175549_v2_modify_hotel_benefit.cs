using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v2_modify_hotel_benefit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel",
                column: "HotelBenefitId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "HotelBenefit");

            migrationBuilder.CreateIndex(
                name: "IX_Hotel_HotelBenefitId",
                table: "Hotel",
                column: "HotelBenefitId");
        }
    }
}
