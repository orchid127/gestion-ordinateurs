using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionOrdinateurs.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifReparation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Technicien",
                table: "Reparations",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Technicien",
                table: "Reparations");
        }
    }
}
