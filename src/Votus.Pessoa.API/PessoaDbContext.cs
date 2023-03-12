using Microsoft.EntityFrameworkCore;

namespace Votus.Pessoa.API
{
    public class PessoaDbContext : DbContext
    {
        public DbSet<Domain.Pessoa> Pessoas { get; set; }

        public PessoaDbContext(DbContextOptions<PessoaDbContext> options) : base(options)
        {

        }

    }
}
