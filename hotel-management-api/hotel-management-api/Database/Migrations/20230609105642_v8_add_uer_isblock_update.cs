using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hotel_management_api.Migrations
{
    public partial class v8_add_uer_isblock_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlock",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlock",
                table: "Users");
        }
    }
}
