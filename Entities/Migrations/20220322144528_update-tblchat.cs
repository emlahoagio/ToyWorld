using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatetblchat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Account_SenderId",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Chat",
                newName: "AccountId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Chat_SenderId",
            //    table: "Chat",
            //    newName: "IX_Chat_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Account_AccountId",
                table: "Chat",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Account_AccountId",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Chat",
                newName: "SenderId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Chat_AccountId",
            //    table: "Chat",
            //    newName: "IX_Chat_SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Account_SenderId",
                table: "Chat",
                column: "SenderId",
                principalTable: "Account",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
