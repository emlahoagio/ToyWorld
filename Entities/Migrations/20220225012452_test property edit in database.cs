using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class testpropertyeditindatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegistrationCost",
                table: "Contest",
                newName: "RegisterCost");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterCost",
                table: "Contest",
                newName: "RegistrationCost");
        }
    }
}
