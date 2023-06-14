using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
