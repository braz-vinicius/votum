using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Infrastructure.Data.Providers;

namespace Votus.Infrastructure.Data.Repositories
{
    public class VoteRepository : SqlRepository<Vote>, IVoteRepository
    {
        public VoteRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}