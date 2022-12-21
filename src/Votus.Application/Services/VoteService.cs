using System;
using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Domain.Services;

namespace Votus.Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }
        public void Vote(string personId, string propositionId, VoteValueType voteValueValue)
        {

            _voteRepository.Add(new Vote()
            {
                PersonId = Guid.Parse(personId),
                PropositionId = propositionId,
                Value = voteValueValue
            });
        }
    }
}
