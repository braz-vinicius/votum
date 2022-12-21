using System.Collections.Generic;
using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Domain.Services;

namespace Votus.Application.Services
{
    public class PropositionService : IPropositionService
    {
        private readonly IPropositionRepository _propositionRepository;

        public PropositionService(IPropositionRepository propositionRepository)
        {
            _propositionRepository = propositionRepository;
        }

        public void CreateOrUpdateProposition(Proposition proposition)
        {
            _propositionRepository.Add(proposition);
        }

        public IEnumerable<Proposition> GetAllPropositions()
        {
            return _propositionRepository.GetAll();
        }
    }
}
