using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BackPuppy.Entity
{
    public class AplicationDbContext:IdentityDbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }
        public AplicationDbContext  ()
        {
        }
        public  DbSet<persona> Personas { get; set; }

        public DbSet<tipoOperador> tipoOperador { get; set; }
        public DbSet<Rol> Roles { get; set; }
        //V VETERINARIO
        //R RECEPCIONISTA
        //A ADINISTRADOR
        //D DUEÑO



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Rol>()
    .HasOne(r => r.Persona)
    .WithMany()
    .HasForeignKey(r => r.IdPersona);

            builder.Entity<Rol>()
                .HasOne(r => r.TipoOperador)
                .WithMany()
                .HasForeignKey(r => r.IdTipoOperador);
        }
    }
}
