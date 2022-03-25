using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class update_tblChat_isRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReaded",
                table: "Chat",
                newName: "IsRead");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
