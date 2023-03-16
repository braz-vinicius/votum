using MediatR;

namespace Votus.Pessoa.API.Events
{
    public class PessoaChangedEvent : INotification
    {
        public PessoaChangedEvent(string id, Domain.Pessoa pessoa)
        {
            Id = id;
            Pessoa = pessoa;
        }

        public string Id { get; }
        public Domain.Pessoa Pessoa { get; }
    }
}
