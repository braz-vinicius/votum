using MediatR;
using NuGet.Protocol.Plugins;

namespace Votus.Proposicao.API.Event
{
    public class ProposicaoChangedEvent : INotification
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
