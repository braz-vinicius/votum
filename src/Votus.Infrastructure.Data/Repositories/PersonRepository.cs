using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Infrastructure.Data.Providers;

namespace Votus.Infrastructure.Data.Repositories
{
    public class PersonRepository : SqlRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public void CreateOrUpdate(Person person)
        {
            var entity = DbSet.Find(person.Id);

            if (entity != null)
                DbSet.Update(person);
        }
    }
}