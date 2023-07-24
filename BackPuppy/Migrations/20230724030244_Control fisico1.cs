using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackPuppy.Migrations
{
    /// <inheritdoc />
    public partial class Controlfisico1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consulta_medica_mascota_id_mascota",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "proxVisita",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.AlterColumn<int>(
                name: "id_mascota",
                schema: "public",
                table: "consulta_medica",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "control_fisico",
                schema: "public",
                table: "consulta_medica",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaProxVisita",
                schema: "public",
                table: "consulta_medica",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "fechaRegistroConsulta",
                schema: "public",
                table: "consulta_medica",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_anamnesis",
                schema: "public",
                table: "consulta_medica",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica",
                column: "controlFisicoid_control_fisico");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_id_anamnesis",
                schema: "public",
                table: "consulta_medica",
                column: "id_anamnesis");

            migrationBuilder.AddForeignKey(
                name: "FK_consulta_medica_ananmnecis_id_anamnesis",
                schema: "public",
                table: "consulta_medica",
                column: "id_anamnesis",
                principalSchema: "public",
                principalTable: "ananmnecis",
                principalColumn: "id_ananmnecis");

            migrationBuilder.AddForeignKey(
                name: "FK_consulta_medica_control_fisico_controlFisicoid_control_fisi~",
                schema: "public",
                table: "consulta_medica",
                column: "controlFisicoid_control_fisico",
                principalSchema: "public",
                principalTable: "control_fisico",
                principalColumn: "id_control_fisico",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_consulta_medica_mascota_id_mascota",
                schema: "public",
                table: "consulta_medica",
                column: "id_mascota",
                principalSchema: "public",
                principalTable: "mascota",
                principalColumn: "id_mascota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_consulta_medica_ananmnecis_id_anamnesis",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropForeignKey(
                name: "FK_consulta_medica_control_fisico_controlFisicoid_control_fisi~",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropForeignKey(
                name: "FK_consulta_medica_mascota_id_mascota",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropIndex(
                name: "IX_consulta_medica_controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropIndex(
                name: "IX_consulta_medica_id_anamnesis",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "controlFisicoid_control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "control_fisico",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "fechaProxVisita",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "fechaRegistroConsulta",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.DropColumn(
                name: "id_anamnesis",
                schema: "public",
                table: "consulta_medica");

            migrationBuilder.AlterColumn<int>(
                name: "id_mascota",
                schema: "public",
                table: "consulta_medica",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "proxVisita",
                schema: "public",
                table: "consulta_medica",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_consulta_medica_mascota_id_mascota",
                schema: "public",
                table: "consulta_medica",
                column: "id_mascota",
                principalSchema: "public",
                principalTable: "mascota",
                principalColumn: "id_mascota",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
