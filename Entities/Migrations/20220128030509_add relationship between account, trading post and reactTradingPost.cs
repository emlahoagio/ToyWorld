using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addrelationshipbetweenaccounttradingpostandreactTradingPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ReactTradingPost_Account",
                table: "ReactTradingPost",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReactTradingPost_TradingPost",
                table: "ReactTradingPost",
                column: "TradingPostId",
                principalTable: "TradingPost",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReactTradingPost_Account",
                table: "ReactTradingPost");

            migrationBuilder.DropForeignKey(
                name: "FK_ReactTradingPost_TradingPost",
                table: "ReactTradingPost");
        }
    }
}
