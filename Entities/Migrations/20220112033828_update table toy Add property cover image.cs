using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatetabletoyAddpropertycoverimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImage",
                table: "Toy",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Toy");
        }
    }
}
