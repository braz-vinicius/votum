using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votus.Common.Data;
using Votus.Domain.Repositories;
using Votus.Proposicao.API;

namespace Votus.Infrastructure.Data.Repositories
{
    public class ProposicaoRepository : SqlRepository<Proposicao.API.Domain.Proposicao>, IProposicaoRepository
    {
        public ProposicaoRepository(ProposicaoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
