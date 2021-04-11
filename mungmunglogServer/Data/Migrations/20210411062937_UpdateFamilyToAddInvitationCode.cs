using Microsoft.EntityFrameworkCore.Migrations;

namespace mungmunglogServer.Data.Migrations
{
    public partial class UpdateFamilyToAddInvitationCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvitationCode",
                table: "Family",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationCode",
                table: "Family");
        }
    }
}
