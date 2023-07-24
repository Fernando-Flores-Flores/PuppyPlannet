using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackPuppy.Migrations
{
    /// <inheritdoc />
    public partial class Controlfisico4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consulta_medica_control_fisico_controlFisicoid_control_fisi~",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropIndex(
                name: "IX_consulta_medica_controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_control_fisico",
                schema: "public",
                table: "consulta_medica",
                column: "control_fisico");

            migrationBuilder.AddForeignKey(
                name: "FK_consulta_medica_control_fisico_control_fisico",
                schema: "public",
                table: "consulta_medica",
                column: "control_fisico",
                principalSchema: "public",
                principalTable: "control_fisico",
                principalColumn: "id_control_fisico");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consulta_medica_control_fisico_control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropIndex(
                name: "IX_consulta_medica_control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.AddColumn<int>(
                name: "controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica",
                column: "controlFisicoid_control_fisico");

            migrationBuilder.AddForeignKey(
                name: "FK_consulta_medica_control_fisico_controlFisicoid_control_fisi~",
                schema: "public",
                table: "consulta_medica",
                column: "controlFisicoid_control_fisico",
                principalSchema: "public",
                principalTable: "control_fisico",
                principalColumn: "id_control_fisico",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
