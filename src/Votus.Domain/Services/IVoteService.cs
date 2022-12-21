using Votus.Domain.Entities;

namespace Votus.Domain.Services
{
    public interface IVoteService
    {
        void Vote(string personId, string propositionId, VoteValueType voteValueValue);

    }
}