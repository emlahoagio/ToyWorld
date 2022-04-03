using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class removeproposalandproposal_prize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contest_Proposal",
                table: "Contest");

            migrationBuilder.DropForeignKey(
                name: "FK_Image_Proposal",
                table: "Image");

            migrationBuilder.DropTable(
                name: "Proposal_Prize");

            migrationBuilder.DropTable(
                name: "Proposal");

            migrationBuilder.DropColumn(
                name: "ProposalId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ProposalId",
                table: "Contest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProposalId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProposalId",
                table: "Contest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Proposal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    ContestDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsWaiting = table.Column<bool>(type: "bit", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxRegister = table.Column<int>(type: "int", nullable: true),
                    MinRegister = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal", x => x.id);
                    table.ForeignKey(
                        name: "FK_Proposal_Account",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposal_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposal_Type",
                        column: x => x.TypeId,
                        principalTable: "Type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proposal_Prize",
                columns: table => new
                {
                    ProposalId = table.Column<int>(type: "int", nullable: false),
                    PrizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposal_Prize", x => new { x.ProposalId, x.PrizeId });
                    table.ForeignKey(
                        name: "FK_Proposal_Prize_Prize",
                        column: x => x.PrizeId,
                        principalTable: "Prize",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proposal_Prize_Proposal",
                        column: x => x.ProposalId,
                        principalTable: "Proposal",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Contest_Proposal",
                table: "Contest",
                column: "ProposalId",
                principalTable: "Proposal",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Proposal",
                table: "Image",
                column: "ProposalId",
                principalTable: "Proposal",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
