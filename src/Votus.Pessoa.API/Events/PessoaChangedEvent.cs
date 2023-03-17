using MediatR;

namespace Votus.Pessoa.API.Events
{
    public class PessoaChangedEvent : INotification
    {
        public PessoaChangedEvent(Guid id, Domain.Pessoa pessoa)
        {
            Id = id;
            Pessoa = pessoa;
        }

        public Guid Id { get; }
        public Domain.Pessoa Pessoa { get; }
    }
}
