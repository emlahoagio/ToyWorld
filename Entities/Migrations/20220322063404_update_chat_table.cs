using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class update_chat_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chat",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_UserId",
                table: "Chat",
                newName: "IX_Chat_AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Chat",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_AccountId",
                table: "Chat",
                newName: "IX_Chat_UserId");
        }
    }
}
