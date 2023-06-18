using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v10_min_max_rating_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_hotel_start_max_value",
                table: "Hotel",
                sql: "Star<=5 AND Star>=1");

            migrationBuilder.AddCheckConstraint(
                name: "CK_comment_rating_max_value",
                table: "Comments",
                sql: "Rating<=5 AND Rating>=1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_hotel_start_max_value",
                table: "Hotel");

            migrationBuilder.DropCheckConstraint(
                name: "CK_comment_rating_max_value",
                table: "Comments");
        }
    }
}
