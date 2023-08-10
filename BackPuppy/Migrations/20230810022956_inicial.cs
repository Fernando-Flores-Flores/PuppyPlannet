using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackPuppy.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "duenos",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombres = table.Column<string>(type: "text", nullable: false),
                    apellido_paterno = table.Column<string>(type: "text", nullable: true),
                    apellido_materno = table.Column<string>(type: "text", nullable: true),
                    telefono = table.Column<int>(type: "integer", nullable: true),
                    correo = table.Column<string>(type: "text", nullable: true),
                    direccion = table.Column<string>(type: "text", nullable: true),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_duenos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "especies",
                schema: "public",
                columns: table => new
                {
                    id_especie = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_especies", x => x.id_especie);
                });

            migrationBuilder.CreateTable(
                name: "personas",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    carnet = table.Column<string>(type: "text", nullable: false),
                    nombres = table.Column<string>(type: "text", nullable: false),
                    apellido_paterno = table.Column<string>(type: "text", nullable: true),
                    apellido_materno = table.Column<string>(type: "text", nullable: true),
                    celular = table.Column<int>(type: "integer", nullable: true),
                    correo = table.Column<string>(type: "text", nullable: true),
                    direccion = table.Column<string>(type: "text", nullable: true),
                    fotografia = table.Column<string>(type: "text", nullable: true),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true),
                    idCuentaIdentity = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "razas",
                schema: "public",
                columns: table => new
                {
                    id_razas = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    IdEspecie = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_razas", x => x.id_razas);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mascota",
                schema: "public",
                columns: table => new
                {
                    id_mascota = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    color = table.Column<string>(type: "text", nullable: false),
                    fecha_nacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    nombreMascota = table.Column<string>(type: "text", nullable: true),
                    sexo = table.Column<string>(type: "text", nullable: false),
                    tatuaje = table.Column<string>(type: "text", nullable: true),
                    conducta = table.Column<string>(type: "text", nullable: true),
                    foto = table.Column<string>(type: "text", nullable: true),
                    idDueno = table.Column<int>(type: "integer", nullable: false),
                    idRaza = table.Column<int>(type: "integer", nullable: false),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mascota", x => x.id_mascota);
                    table.ForeignKey(
                        name: "FK_mascota_duenos_idDueno",
                        column: x => x.idDueno,
                        principalSchema: "public",
                        principalTable: "duenos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mascota_razas_idRaza",
                        column: x => x.idRaza,
                        principalSchema: "public",
                        principalTable: "razas",
                        principalColumn: "id_razas",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ananmnecis",
                schema: "public",
                columns: table => new
                {
                    id_ananmnecis = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    apetito = table.Column<string>(type: "text", nullable: true),
                    agua = table.Column<string>(type: "text", nullable: true),
                    conducta = table.Column<string>(type: "text", nullable: true),
                    defecacion = table.Column<string>(type: "text", nullable: true),
                    alteracionesRes = table.Column<string>(type: "text", nullable: true),
                    alteracionesNeuro = table.Column<string>(type: "text", nullable: true),
                    problemasUr = table.Column<string>(type: "text", nullable: true),
                    id_mascota = table.Column<int>(type: "integer", nullable: false),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ananmnecis", x => x.id_ananmnecis);
                    table.ForeignKey(
                        name: "FK_ananmnecis_mascota_id_mascota",
                        column: x => x.id_mascota,
                        principalSchema: "public",
                        principalTable: "mascota",
                        principalColumn: "id_mascota",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "consulta_medica",
                schema: "public",
                columns: table => new
                {
                    id_consulta_medica = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    motivo_consulta = table.Column<string>(type: "text", nullable: true),
                    diagnostico_consulta = table.Column<string>(type: "text", nullable: true),
                    tratamiento = table.Column<string>(type: "text", nullable: true),
                    fecha_prox_visita = table.Column<string>(type: "text", nullable: true),
                    fecha_registro_consulta = table.Column<string>(type: "text", nullable: true),
                    id_anamnesis = table.Column<int>(type: "integer", nullable: true),
                    id_control_fisico = table.Column<int>(type: "integer", nullable: true),
                    id_mascota = table.Column<int>(type: "integer", nullable: true),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consulta_medica", x => x.id_consulta_medica);
                    table.ForeignKey(
                        name: "FK_consulta_medica_ananmnecis_id_anamnesis",
                        column: x => x.id_anamnesis,
                        principalSchema: "public",
                        principalTable: "ananmnecis",
                        principalColumn: "id_ananmnecis");
                    table.ForeignKey(
                        name: "FK_consulta_medica_control_fisico_id_control_fisico",
                        column: x => x.id_control_fisico,
                        principalSchema: "public",
                        principalTable: "control_fisico",
                        principalColumn: "id_control_fisico");
                    table.ForeignKey(
                        name: "FK_consulta_medica_mascota_id_mascota",
                        column: x => x.id_mascota,
                        principalSchema: "public",
                        principalTable: "mascota",
                        principalColumn: "id_mascota");
                });

            migrationBuilder.CreateTable(
                name: "desparacitaciones",
                schema: "public",
                columns: table => new
                {
                    id_desparacitacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha_desparacitacion = table.Column<string>(type: "text", nullable: true),
                    fecha_proxima_desparacitacion = table.Column<string>(type: "text", nullable: true),
                    principio_activo = table.Column<string>(type: "text", nullable: true),
                    producto_desparacitacion = table.Column<string>(type: "text", nullable: true),
                    tipo_desparacitacion = table.Column<string>(type: "text", nullable: true),
                    via_desparacitcion = table.Column<string>(type: "text", nullable: true),
                    id_mascota = table.Column<int>(type: "integer", nullable: true),
                    id_control_fisico = table.Column<int>(type: "integer", nullable: true),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_desparacitaciones", x => x.id_desparacitacion);
                    table.ForeignKey(
                        name: "FK_desparacitaciones_control_fisico_id_control_fisico",
                        column: x => x.id_control_fisico,
                        principalSchema: "public",
                        principalTable: "control_fisico",
                        principalColumn: "id_control_fisico");
                    table.ForeignKey(
                        name: "FK_desparacitaciones_mascota_id_mascota",
                        column: x => x.id_mascota,
                        principalSchema: "public",
                        principalTable: "mascota",
                        principalColumn: "id_mascota");
                });

            migrationBuilder.CreateTable(
                name: "vacunas",
                schema: "public",
                columns: table => new
                {
                    id_vacuna = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion_vacuna = table.Column<string>(type: "text", nullable: false),
                    laboratorio = table.Column<string>(type: "text", nullable: true),
                    fecha_vacunacion = table.Column<string>(type: "text", nullable: false),
                    fecha_revacunacion = table.Column<string>(type: "text", nullable: false),
                    id_mascota = table.Column<int>(type: "integer", nullable: true),
                    id_control_fisico = table.Column<int>(type: "integer", nullable: true),
                    api_estado = table.Column<string>(type: "text", nullable: true),
                    api_transaccion = table.Column<string>(type: "text", nullable: true),
                    fecha_cre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    fecha_mod = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    usuario_mod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vacunas", x => x.id_vacuna);
                    table.ForeignKey(
                        name: "FK_vacunas_control_fisico_id_control_fisico",
                        column: x => x.id_control_fisico,
                        principalSchema: "public",
                        principalTable: "control_fisico",
                        principalColumn: "id_control_fisico");
                    table.ForeignKey(
                        name: "FK_vacunas_mascota_id_mascota",
                        column: x => x.id_mascota,
                        principalSchema: "public",
                        principalTable: "mascota",
                        principalColumn: "id_mascota");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ananmnecis_id_mascota",
                schema: "public",
                table: "ananmnecis",
                column: "id_mascota");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_id_anamnesis",
                schema: "public",
                table: "consulta_medica",
                column: "id_anamnesis");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_id_control_fisico",
                schema: "public",
                table: "consulta_medica",
                column: "id_control_fisico");

            migrationBuilder.CreateIndex(
                name: "IX_consulta_medica_id_mascota",
                schema: "public",
                table: "consulta_medica",
                column: "id_mascota");

            migrationBuilder.CreateIndex(
                name: "IX_control_fisico_id_mascota",
                schema: "public",
                table: "control_fisico",
                column: "id_mascota");

            migrationBuilder.CreateIndex(
                name: "IX_desparacitaciones_id_control_fisico",
                schema: "public",
                table: "desparacitaciones",
                column: "id_control_fisico");

            migrationBuilder.CreateIndex(
                name: "IX_desparacitaciones_id_mascota",
                schema: "public",
                table: "desparacitaciones",
                column: "id_mascota");

            migrationBuilder.CreateIndex(
                name: "IX_mascota_idDueno",
                schema: "public",
                table: "mascota",
                column: "idDueno");

            migrationBuilder.CreateIndex(
                name: "IX_mascota_idRaza",
                schema: "public",
                table: "mascota",
                column: "idRaza");

            migrationBuilder.CreateIndex(
                name: "IX_vacunas_id_control_fisico",
                schema: "public",
                table: "vacunas",
                column: "id_control_fisico");

            migrationBuilder.CreateIndex(
                name: "IX_vacunas_id_mascota",
                schema: "public",
                table: "vacunas",
                column: "id_mascota");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "consulta_medica",
                schema: "public");

            migrationBuilder.DropTable(
                name: "desparacitaciones",
                schema: "public");

            migrationBuilder.DropTable(
                name: "especies",
                schema: "public");

            migrationBuilder.DropTable(
                name: "personas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "vacunas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ananmnecis",
                schema: "public");

            migrationBuilder.DropTable(
                name: "control_fisico",
                schema: "public");

            migrationBuilder.DropTable(
                name: "mascota",
                schema: "public");

            migrationBuilder.DropTable(
                name: "duenos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "razas",
                schema: "public");
        }
    }
}
