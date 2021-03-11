using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class AddWalkPathAndConnectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Family_FamilyId",
                table: "Pet");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Pet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WalkHistory",
                columns: table => new
                {
                    WalkHistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
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
                    table.PrimaryKey("PK_WalkHistory", x => x.WalkHistoryId);
                    table.ForeignKey(
                        name: "FK_WalkHistory_FamilyMember_FamilyMemberId",
                        column: x => x.FamilyMemberId,
                        principalTable: "FamilyMember",
                        principalColumn: "FamilyMemberId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WalkHistory_Pet_PetId",
                        column: x => x.PetId,
                        principalTable: "Pet",
                        principalColumn: "PetId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WalkPath",
                columns: table => new
                {
                    WalkPathId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    LocationBasedTime = table.Column<DateTime>(nullable: false),
                    WalkHistoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WalkPath", x => x.WalkPathId);
                    table.ForeignKey(
                        name: "FK_WalkPath_WalkHistory_WalkHistoryId",
                        column: x => x.WalkHistoryId,
                        principalTable: "WalkHistory",
                        principalColumn: "WalkHistoryId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WalkHistory_FamilyMemberId",
                table: "WalkHistory",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkHistory_PetId",
                table: "WalkHistory",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_WalkPath_WalkHistoryId",
                table: "WalkPath",
                column: "WalkHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Family_FamilyId",
                table: "Pet",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.NoAction);
            // Noaction
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pet_Family_FamilyId",
                table: "Pet");

            migrationBuilder.DropTable(
                name: "WalkPath");

            migrationBuilder.DropTable(
                name: "WalkHistory");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "Pet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_Family_FamilyId",
                table: "Pet",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
            //No action
        }
    }
}
