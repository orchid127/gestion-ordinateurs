using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionOrdinateurs.API.Migrations
{
    /// <inheritdoc />
    public partial class AjoutCaracteristiques : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Processeur",
                table: "Ordinateurs");

            migrationBuilder.DropColumn(
                name: "Ram",
                table: "Ordinateurs");

            migrationBuilder.RenameColumn(
                name: "Marque",
                table: "Ordinateurs",
                newName: "Caracteristiques_Marque");

            migrationBuilder.AddColumn<int>(
                name: "Caracteristiques_RamGo",
                table: "Ordinateurs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Caracteristiques_StockageGo",
                table: "Ordinateurs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Caracteristiques_TypeOrdi",
                table: "Ordinateurs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caracteristiques_RamGo",
                table: "Ordinateurs");

            migrationBuilder.DropColumn(
                name: "Caracteristiques_StockageGo",
                table: "Ordinateurs");

            migrationBuilder.DropColumn(
                name: "Caracteristiques_TypeOrdi",
                table: "Ordinateurs");

            migrationBuilder.RenameColumn(
                name: "Caracteristiques_Marque",
                table: "Ordinateurs",
                newName: "Marque");

            migrationBuilder.AddColumn<string>(
                name: "Processeur",
                table: "Ordinateurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ram",
                table: "Ordinateurs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
