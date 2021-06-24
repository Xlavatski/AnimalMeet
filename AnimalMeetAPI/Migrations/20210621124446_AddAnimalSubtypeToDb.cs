using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalMeetAPI.Migrations
{
    public partial class AddAnimalSubtypeToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalSubtypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnimalTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalSubtypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalSubtypes_Animals_AnimalTypeId",
                        column: x => x.AnimalTypeId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalSubtypes_AnimalTypeId",
                table: "AnimalSubtypes",
                column: "AnimalTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalSubtypes");
        }
    }
}
