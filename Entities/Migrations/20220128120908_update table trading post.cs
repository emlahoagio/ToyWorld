using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatetabletradingpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "TradingPost",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<int>(
                name: "TradingPostId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_TradingPost_TradingPostId",
                table: "Comment",
                column: "TradingPostId",
                principalTable: "TradingPost",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_TradingPost_TradingPostId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "TradingPost");

            migrationBuilder.DropColumn(
                name: "TradingPostId",
                table: "Comment");
        }
    }
}
