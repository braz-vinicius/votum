using Votus.Common.Data;
using Votus.Voto.API;

namespace Votus.Voto.API.Repository
{
    public class VotoRepository : SqlRepository<Domain.Voto>, IVotoRepository
    {
        public VotoRepository(VotoDbContext dbContext) : base(dbContext)
        {
        }
    }
}