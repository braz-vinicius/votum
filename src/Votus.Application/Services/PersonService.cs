using Votus.Domain.Abstractions;
using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Domain.Services;

namespace Votus.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void CreatePerson(IApplicationUser user)
        {
            _personRepository.Add(new Person
            {
                Id = user.Id,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                UserName = user.UserName
            });

            
        }

        public void UpdatePerson(IApplicationUser user)
        {
            _personRepository.CreateOrUpdate(new Person
            {
                Id = user.Id,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                UserName = user.UserName
            });
        }
    }
}