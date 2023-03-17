using Microsoft.EntityFrameworkCore;
using Votus.Common.ServiceBus;
using Votus.Voto.API.Event;

namespace Votus.Voto.API.EventHandlers
{
    public class ProposicaoChangedEventHandler : IEventHandler
    {
        private VotoDbContext dbContext;

        public ProposicaoChangedEventHandler(VotoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Handle(object eventModel)
        {
            var proposicaoEvent = (ProposicaoChangedEvent) eventModel;

            var voto = dbContext.Votos.SingleOrDefault(x=> x.ProposicaoId == proposicaoEvent.id);

            if (voto != null)
            {
                voto.ProposicaoNome = proposicaoEvent.newProposicao.Questao;

                dbContext.Entry(voto).State = EntityState.Modified;
                
                dbContext.SaveChanges();
            }
        }
    }
}
