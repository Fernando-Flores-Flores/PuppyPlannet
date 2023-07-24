using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackPuppy.Migrations
{
    /// <inheritdoc />
    public partial class Controlfisico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "control_medico",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "control_fisico",
                schema: "public",
                columns: table => new
                {
                    id_control_fisico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    temperatura = table.Column<string>(type: "text", nullable: true),
                    frecCardiaca = table.Column<string>(type: "text", nullable: true),
                    frecRespiratoria = table.Column<string>(type: "text", nullable: true),
                    peso = table.Column<string>(type: "text", nullable: true),
                    id_mascota = table.Column<int>(type: "integer", nullable: false),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_fisico", x => x.id_control_fisico);
                    table.ForeignKey(
                        name: "FK_control_fisico_mascota_id_mascota",
                        column: x => x.id_mascota,
                        principalSchema: "public",
                        principalTable: "mascota",
                        principalColumn: "id_mascota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_control_fisico_id_mascota",
                schema: "public",
                table: "control_fisico",
                column: "id_mascota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "control_fisico",
                schema: "public");

            migrationBuilder.CreateTable(
                name: "control_medico",
                schema: "public",
                columns: table => new
                {
                    id_control_medico = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_mascota = table.Column<int>(type: "integer", nullable: false),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    frecCardiaca = table.Column<string>(type: "text", nullable: true),
                    frecRespiratoria = table.Column<string>(type: "text", nullable: true),
                    peso = table.Column<string>(type: "text", nullable: true),
                    temperatura = table.Column<string>(type: "text", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_control_medico", x => x.id_control_medico);
                    table.ForeignKey(
                        name: "FK_control_medico_mascota_id_mascota",
                        column: x => x.id_mascota,
                        principalSchema: "public",
                        principalTable: "mascota",
                        principalColumn: "id_mascota",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_control_medico_id_mascota",
                schema: "public",
                table: "control_medico",
                column: "id_mascota");
        }
    }
}
