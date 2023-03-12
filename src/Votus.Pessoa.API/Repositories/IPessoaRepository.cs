using Votus.Common.Data;

namespace Votus.Pessoa.API.Repositories
{
    public interface IPessoaRepository : IRepository<Domain.Pessoa>
    {
        void CreateOrUpdate(Domain.Pessoa person);
    }
}