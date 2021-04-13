using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class UpdateFamilyToAddCodeExpirationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeExpirationDate",
                table: "Family",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExpirationDate",
                table: "Family");
        }
    }
}
