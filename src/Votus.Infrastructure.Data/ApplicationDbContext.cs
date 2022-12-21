using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Votus.Domain.Entities;
using Votus.Infrastructure.Identity;

namespace Votus.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<WebApplicationUser>
    {

        public DbSet<Post> Posts { get; set; }

        public DbSet<Proposition> Propositions { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public ApplicationDbContext()
        {
                
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning tech debt - replace hard coded strings
            optionsBuilder.UseSqlServer(@"Server=tcp:votus.database.windows.net,1433;Initial Catalog=Votus;Persist Security Info=False;User ID=votus;Password=xhn!BvST5KnJ4j;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
