using Microsoft.EntityFrameworkCore;

namespace Votus.Voto.API
{
    public class VotoDbContext : DbContext
    {
        public DbSet<Domain.Voto> Votos { get; set; }

        public VotoDbContext(DbContextOptions<VotoDbContext> options) : base(options) 
        {
            
        }
    }
}
