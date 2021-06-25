using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalMeetAPI.Migrations
{
    public partial class AddPasAUserToUserDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ApplicationUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "ApplicationUsers");
        }
    }
}
