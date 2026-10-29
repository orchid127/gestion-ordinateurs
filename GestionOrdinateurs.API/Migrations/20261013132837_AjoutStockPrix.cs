using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionOrdinateurs.API.Migrations
{
    /// <inheritdoc />
    public partial class AjoutStockPrix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Prix",
                table: "Ordinateurs",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Ordinateurs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prix",
                table: "Ordinateurs");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Ordinateurs");
        }
    }
}
