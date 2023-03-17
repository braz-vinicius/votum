using Microsoft.EntityFrameworkCore;
using Votus.Common.ServiceBus;
using Votus.Proposicao.API.Event;

namespace Votus.Proposicao.API.EventHandler
{
    public class PessoaChangedEventHandler : IEventHandler
    {
        private readonly ProposicaoDbContext context;

        public PessoaChangedEventHandler(ProposicaoDbContext context)
        {
            this.context = context;
        }
        public void Handle(object eventModel)
        {
            var domainEvent = (PessoaChangedEvent)eventModel;

            var pessoa = context.Proposicoes.SingleOrDefault(x => x.PessoaId == domainEvent.Id);

            if (pessoa != null)
            {
                pessoa.PessoaNome = domainEvent.Pessoa.NomeCompleto;

                context.Entry(pessoa).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
    }
}
