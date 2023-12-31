﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BackPuppy.Entity
{
    public class AplicationDbContext : IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public AplicationDbContext()
        {
        }
        public DbSet<persona> Personas { get; set; }
        public DbSet<mascota> Mascota { get; set; }
        public DbSet<razas> Raza { get; set; }
        public DbSet<especies> Especie { get; set; }
        public DbSet<control_fisico> controlFisico { get; set; }
        public DbSet<ananmnecis> Anamnecis { get; set; }
        public DbSet<consulta_medica> consultaMedica { get; set; }
        public DbSet<vacunas> vacunas { get; set; }
        public DbSet<desparacitaciones> Desparacitacion{ get; set; }
        public DbSet<primera_consulta> PrimeraConsulta { get; set; }
        public DbSet<cirugia> Cirugia{ get; set; }






        //public DbSet<tipoOperador> tipoOperador { get; set; }
        //public DbSet<Rol> Roles { get; set; }
        //V VETERINARIO
        //R RECEPCIONISTA
        //A ADINISTRADOR
        //D DUEÑO
        public DbSet<duenos> Duenos { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
    }
}
