using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class changecreatetimeofbilltoupdatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Bill",
                newName: "UpdateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateTime",
                table: "Bill",
                newName: "CreateTime");
        }
    }
}
