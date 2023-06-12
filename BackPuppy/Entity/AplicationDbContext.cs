using Microsoft.EntityFrameworkCore;

namespace BackPuppy.Entity
{
    public class AplicationDbContext:DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
                
        }
        public AplicationDbContext  ()
        {
        }
        public  DbSet<persona> Personas { get; set; }
    }
}
