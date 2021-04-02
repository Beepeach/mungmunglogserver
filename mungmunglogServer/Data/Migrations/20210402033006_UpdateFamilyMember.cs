using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class UpdateFamilyMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMember_Family_FamilyId",
                table: "FamilyMember");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "FamilyMember",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMember_Family_FamilyId",
                table: "FamilyMember",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMember_Family_FamilyId",
                table: "FamilyMember");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "FamilyMember",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMember_Family_FamilyId",
                table: "FamilyMember",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
