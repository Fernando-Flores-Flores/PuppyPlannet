using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackPuppy.Migrations
{
    /// <inheritdoc />
    public partial class Controlfisico6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "motivo_consu1ta",
                schema: "public",
                table: "consulta_medica",
                newName: "motivo_consulta");

            migrationBuilder.RenameColumn(
                name: "diagnostico_consu1ta",
                schema: "public",
                table: "consulta_medica",
                newName: "diagnostico_consulta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "motivo_consulta",
                schema: "public",
                table: "consulta_medica",
                newName: "motivo_consu1ta");

            migrationBuilder.RenameColumn(
                name: "diagnostico_consulta",
                schema: "public",
                table: "consulta_medica",
                newName: "diagnostico_consu1ta");
        }
    }
}
