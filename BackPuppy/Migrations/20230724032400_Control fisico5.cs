using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackPuppy.Migrations
{
    /// <inheritdoc />
    public partial class Controlfisico5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "motivoConsu1ta",
                schema: "public",
                table: "consulta_medica",
                newName: "motivo_consu1ta");

            migrationBuilder.RenameColumn(
                name: "fechaRegistroConsulta",
                schema: "public",
                table: "consulta_medica",
                newName: "fecha_registro_consulta");

            migrationBuilder.RenameColumn(
                name: "fechaProxVisita",
                schema: "public",
                table: "consulta_medica",
                newName: "fecha_prox_visita");

            migrationBuilder.RenameColumn(
                name: "diagnosticoConsu1ta",
                schema: "public",
                table: "consulta_medica",
                newName: "diagnostico_consu1ta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "motivo_consu1ta",
                schema: "public",
                table: "consulta_medica",
                newName: "motivoConsu1ta");

            migrationBuilder.RenameColumn(
                name: "fecha_registro_consulta",
                schema: "public",
                table: "consulta_medica",
                newName: "fechaRegistroConsulta");

            migrationBuilder.RenameColumn(
                name: "fecha_prox_visita",
                schema: "public",
                table: "consulta_medica",
                newName: "fechaProxVisita");

            migrationBuilder.RenameColumn(
                name: "diagnostico_consu1ta",
                schema: "public",
                table: "consulta_medica",
                newName: "diagnosticoConsu1ta");
        }
    }
}
