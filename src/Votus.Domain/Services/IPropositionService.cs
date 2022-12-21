using System.Collections.Generic;
using Votus.Domain.Entities;

namespace Votus.Domain.Services
{
    public interface IPropositionService
    {
        void CreateOrUpdateProposition(Proposition proposition);
        IEnumerable<Proposition> GetAllPropositions();
    }
}