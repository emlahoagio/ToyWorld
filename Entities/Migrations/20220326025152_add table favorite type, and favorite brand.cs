using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addtablefavoritetypeandfavoritebrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteBrand",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteBrand", x => new { x.AccountId, x.BrandId });
                    table.ForeignKey(
                        name: "FK_Brand_Account_FavoriteBrandAccount",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Type_Account_FavoriteBrandBrand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteType",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteType", x => new { x.AccountId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_Type_Account_FavoriteTypeAccount",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Type_Account_FavoriteTypeType",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteBrand");

            migrationBuilder.DropTable(
                name: "FavoriteType");
        }
    }
}
