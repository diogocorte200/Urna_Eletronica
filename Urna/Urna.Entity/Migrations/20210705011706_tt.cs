using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Urna.Entity.Migrations
{
    public partial class tt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidatos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NomeCompleto = table.Column<string>(nullable: true),
                    NomeVice = table.Column<string>(nullable: true),
                    DataRegistro = table.Column<DateTime>(nullable: false),
                    Legenda = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "votos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataVoto = table.Column<DateTime>(nullable: false),
                    IdCandidato = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_votos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidatos");

            migrationBuilder.DropTable(
                name: "votos");
        }
    }
}
