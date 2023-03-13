using Microsoft.EntityFrameworkCore;

namespace Votus.Proposicao.API
{
    public class ProposicaoDbContext : DbContext
    {
        public DbSet<Domain.Proposicao> Proposicoes { get; set; }

        public ProposicaoDbContext(DbContextOptions<ProposicaoDbContext> options) : base(options)
        {
            
        }
    }
}
