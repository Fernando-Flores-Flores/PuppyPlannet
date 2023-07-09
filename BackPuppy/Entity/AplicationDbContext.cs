using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
