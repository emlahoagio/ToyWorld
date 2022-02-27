using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatetabletradingpostaddcolumntypeidandbrandid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "TradingPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "TradingPost",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TradingPost_Brand",
                table: "TradingPost",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TradingPost_Type",
                table: "TradingPost",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradingPost_Brand",
                table: "TradingPost");

            migrationBuilder.DropForeignKey(
                name: "FK_TradingPost_Type",
                table: "TradingPost");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "TradingPost");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TradingPost");
        }
    }
}
