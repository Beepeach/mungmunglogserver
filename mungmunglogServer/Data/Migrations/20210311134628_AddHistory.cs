using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class AddHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    HistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Contents = table.Column<string>(maxLength: 1000, nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    FileUrl1 = table.Column<string>(nullable: true),
                    FileUrl2 = table.Column<string>(nullable: true),
                    FileUrl3 = table.Column<string>(nullable: true),
                    FileUrl4 = table.Column<string>(nullable: true),
                    FileUrl5 = table.Column<string>(nullable: true),
                    PetId = table.Column<int>(nullable: false),
                    FamilyMemberId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.HistoryId);
                    table.ForeignKey(
                        name: "FK_History_FamilyMember_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMember",
                        principalColumn: "FamilyMemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_FamilyMemberId",
                table: "History",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_History_PetId",
                table: "History",
                column: "PetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");
        }
    }
}
