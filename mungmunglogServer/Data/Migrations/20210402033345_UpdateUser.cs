using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Family_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Family_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Family_FamilyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "FamilyId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Family_FamilyId",
                table: "AspNetUsers",
                column: "FamilyId",
                principalTable: "Family",
                principalColumn: "FamilyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
