using MediatR;
using NuGet.Protocol.Plugins;

namespace Votus.Voto.API.Event
{
    public class ProposicaoChangedEvent
    {

        public ProposicaoChangedEvent(string id, Domain.Proposicao newProposicao)
        {
            this.id = id;
            this.newProposicao = newProposicao;
        }

        public string id { get; }
        public Domain.Proposicao newProposicao { get; private set; }
    }
}
