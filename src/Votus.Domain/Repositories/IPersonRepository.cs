using Votus.Domain.Abstractions;
using Votus.Domain.Entities;

namespace Votus.Domain.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        void CreateOrUpdate(Person person);
    }
}