using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionOrdinateurs.API.Migrations
{
    /// <inheritdoc />
    public partial class AjoutReparation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reparations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UnOrdinateurId = table.Column<int>(type: "INTEGER", nullable: true),
                    TypeReparation = table.Column<string>(type: "TEXT", nullable: true),
                    DateDebut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Statut = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparations_Ordinateurs_UnOrdinateurId",
                        column: x => x.UnOrdinateurId,
                        principalTable: "Ordinateurs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reparations_UnOrdinateurId",
                table: "Reparations",
                column: "UnOrdinateurId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reparations");
        }
    }
}
