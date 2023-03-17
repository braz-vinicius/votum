using Microsoft.EntityFrameworkCore;
using Votus.Common.ServiceBus;
using Votus.Voto.API.Event;

namespace Votus.Voto.API.EventHandlers
{
    public class PessoaChangedEventHandler : IEventHandler
    {
        private readonly VotoDbContext context;

        public PessoaChangedEventHandler(VotoDbContext context)
        {
            this.context = context;
        }
        public void Handle(object eventModel)
        {
            var domainEvent = (PessoaChangedEvent)eventModel;

            var voto = context.Votos.SingleOrDefault(x => x.PessoaId == new Guid(domainEvent.Id));

            if (voto != null)
            {
                voto.PessoaNome = domainEvent.Pessoa.NomeCompleto;

                context.Entry(voto).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
    }
}
