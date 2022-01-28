using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addnewtablereacttradingpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "ReactTradingPost",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    TradingPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactTradingPost", x => new { x.AccountId, x.TradingPostId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReactTradingPost");
        }
    }
}
