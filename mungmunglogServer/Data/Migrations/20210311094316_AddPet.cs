using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class AddPet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pet",
                columns: table => new
                {
                    PetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 10, nullable: false),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Breed = table.Column<string>(nullable: false),
                    Gender = table.Column<bool>(nullable: false),
                    FileUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pet", x => x.PetId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pet");
        }
    }
}
