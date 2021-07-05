using Microsoft.EntityFrameworkCore.Migrations;

namespace Urna.Entity.Migrations
{
    public partial class te : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeVice",
                table: "candidatos");

            migrationBuilder.AddColumn<string>(
                name: "ViceCandidato",
                table: "candidatos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViceCandidato",
                table: "candidatos");

            migrationBuilder.AddColumn<string>(
                name: "NomeVice",
                table: "candidatos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
