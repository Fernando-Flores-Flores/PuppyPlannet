﻿// <auto-generated />
using System;
using BackPuppy.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackPuppy.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20230724023712_Control fisico")]
    partial class Controlfisico
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackPuppy.Entity.ananmnecis", b =>
                {
                    b.Property<int>("id_ananmnecis")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_ananmnecis"));

                    b.Property<string>("agua")
                        .HasColumnType("text");

                    b.Property<string>("alteracionesNeuro")
                        .HasColumnType("text");

                    b.Property<string>("alteracionesRes")
                        .HasColumnType("text");

                    b.Property<string>("apetito")
                        .HasColumnType("text");

                    b.Property<string>("api_estado")
                        .HasColumnType("text");

                    b.Property<string>("api_transaccion")
                        .HasColumnType("text");

                    b.Property<string>("conducta")
                        .HasColumnType("text");

                    b.Property<string>("defecacion")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_cre")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_mod")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("id_mascota")
                        .HasColumnType("integer");

                    b.Property<string>("problemasUr")
                        .HasColumnType("text");

                    b.Property<string>("usuario_mod")
                        .HasColumnType("text");

                    b.HasKey("id_ananmnecis");

                    b.HasIndex("id_mascota");

                    b.ToTable("ananmnecis", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.consulta_medica", b =>
                {
                    b.Property<int>("id_consulta_medica")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_consulta_medica"));

                    b.Property<string>("api_estado")
                        .HasColumnType("text");

                    b.Property<string>("api_transaccion")
                        .HasColumnType("text");

                    b.Property<string>("diagnosticoConsu1ta")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_cre")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_mod")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("id_mascota")
                        .HasColumnType("integer");

                    b.Property<string>("motivoConsu1ta")
                        .HasColumnType("text");

                    b.Property<string>("proxVisita")
                        .HasColumnType("text");

                    b.Property<string>("tratamiento")
                        .HasColumnType("text");

                    b.Property<string>("usuario_mod")
                        .HasColumnType("text");

                    b.HasKey("id_consulta_medica");

                    b.HasIndex("id_mascota");

                    b.ToTable("consulta_medica", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.control_fisico", b =>
                {
                    b.Property<int>("id_control_fisico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id_control_fisico"));

                    b.Property<string>("api_estado")
                        .HasColumnType("text");

                    b.Property<string>("api_transaccion")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_cre")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_mod")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("frecCardiaca")
                        .HasColumnType("text");

                    b.Property<string>("frecRespiratoria")
                        .HasColumnType("text");

                    b.Property<int>("id_mascota")
                        .HasColumnType("integer");

                    b.Property<string>("peso")
                        .HasColumnType("text");

                    b.Property<string>("temperatura")
                        .HasColumnType("text");

                    b.Property<string>("usuario_mod")
                        .HasColumnType("text");

                    b.HasKey("id_control_fisico");

                    b.HasIndex("id_mascota");

                    b.ToTable("control_fisico", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.duenos", b =>
                {
                    b.Property<int>("IdDuenos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdDuenos"));

                    b.Property<string>("apellidoMaterno")
                        .HasColumnType("text")
                        .HasColumnName("apellido_materno");

                    b.Property<string>("apellidoPaterno")
                        .HasColumnType("text")
                        .HasColumnName("apellido_paterno");

                    b.Property<string>("api_estado")
                        .HasColumnType("text");

                    b.Property<string>("api_transaccion")
                        .HasColumnType("text");

                    b.Property<string>("correo")
                        .HasColumnType("text");

                    b.Property<string>("direccion")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_cre")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_mod")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("nombres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("telefono")
                        .HasColumnType("integer");

                    b.Property<string>("usuario_mod")
                        .HasColumnType("text");

                    b.HasKey("IdDuenos");

                    b.ToTable("duenos", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.especies", b =>
                {
                    b.Property<int>("IdEspecie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_especie");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdEspecie"));

                    b.Property<int>("IdRaza")
                        .HasColumnType("integer");

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("IdEspecie");

                    b.HasIndex("IdRaza");

                    b.ToTable("especies", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.mascota", b =>
                {
                    b.Property<int>("idMascota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_mascota");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idMascota"));

                    b.Property<string>("api_estado")
                        .HasColumnType("text");

                    b.Property<string>("api_transaccion")
                        .HasColumnType("text");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("conducta")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_cre")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_mod")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_nacimiento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("foto")
                        .HasColumnType("text");

                    b.Property<int>("idDueno")
                        .HasColumnType("integer");

                    b.Property<int>("idEspecie")
                        .HasColumnType("integer");

                    b.Property<string>("nombreMascota")
                        .HasColumnType("text");

                    b.Property<string>("sexo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("tatuaje")
                        .HasColumnType("text");

                    b.Property<string>("usuario_mod")
                        .HasColumnType("text");

                    b.HasKey("idMascota");

                    b.HasIndex("idDueno");

                    b.HasIndex("idEspecie");

                    b.ToTable("mascota", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.persona", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("apellidoMaterno")
                        .HasColumnType("text")
                        .HasColumnName("apellido_materno");

                    b.Property<string>("apellidoPaterno")
                        .HasColumnType("text")
                        .HasColumnName("apellido_paterno");

                    b.Property<string>("api_estado")
                        .HasColumnType("text");

                    b.Property<string>("api_transaccion")
                        .HasColumnType("text");

                    b.Property<string>("carnet")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("carnet");

                    b.Property<int?>("celular")
                        .HasColumnType("integer");

                    b.Property<string>("correo")
                        .HasColumnType("text");

                    b.Property<string>("direccion")
                        .HasColumnType("text");

                    b.Property<DateTime?>("fecha_cre")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("fecha_mod")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("fotografia")
                        .HasColumnType("text");

                    b.Property<string>("idCuentaIdentity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("nombres")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("usuario_mod")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("personas", "public");
                });

            modelBuilder.Entity("BackPuppy.Entity.razas", b =>
                {
                    b.Property<int>("IdRaza")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id_razas");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdRaza"));

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdRaza");

                    b.ToTable("razas", "public");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BackPuppy.Entity.ananmnecis", b =>
                {
                    b.HasOne("BackPuppy.Entity.mascota", "mascota")
                        .WithMany()
                        .HasForeignKey("id_mascota")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("mascota");
                });

            modelBuilder.Entity("BackPuppy.Entity.consulta_medica", b =>
                {
                    b.HasOne("BackPuppy.Entity.mascota", "mascota")
                        .WithMany()
                        .HasForeignKey("id_mascota")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("mascota");
                });

            modelBuilder.Entity("BackPuppy.Entity.control_fisico", b =>
                {
                    b.HasOne("BackPuppy.Entity.mascota", "mascota")
                        .WithMany()
                        .HasForeignKey("id_mascota")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("mascota");
                });

            modelBuilder.Entity("BackPuppy.Entity.especies", b =>
                {
                    b.HasOne("BackPuppy.Entity.razas", "Raza")
                        .WithMany()
                        .HasForeignKey("IdRaza")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Raza");
                });

            modelBuilder.Entity("BackPuppy.Entity.mascota", b =>
                {
                    b.HasOne("BackPuppy.Entity.duenos", "Dueno")
                        .WithMany()
                        .HasForeignKey("idDueno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackPuppy.Entity.especies", "Especie")
                        .WithMany()
                        .HasForeignKey("idEspecie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dueno");

                    b.Navigation("Especie");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
