using Votus.Domain.Abstractions;

namespace Votus.Domain.Services
{
    public interface IPersonService
    {
        void CreatePerson(IApplicationUser user);
        void UpdatePerson(IApplicationUser user);
    }
}