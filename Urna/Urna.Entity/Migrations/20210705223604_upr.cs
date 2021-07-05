using Microsoft.EntityFrameworkCore.Migrations;

namespace Urna.Entity.Migrations
{
    public partial class upr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "candidatos");

            migrationBuilder.AddColumn<string>(
                name: "NomeCandidato",
                table: "candidatos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeCandidato",
                table: "candidatos");

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "candidatos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
