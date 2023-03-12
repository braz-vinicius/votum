using Votus.Common.Data;
using Votus.Pessoa.API;

namespace Votus.Pessoa.API.Repositories
{
    public class PessoaRepository : SqlRepository<Domain.Pessoa>, IPessoaRepository
    {
        public PessoaRepository(PessoaDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOrUpdate(Domain.Pessoa person)
        {
            var entity = DbSet.Find(person.Id);

            if (entity != null)
                DbSet.Update(person);
        }
    }
}