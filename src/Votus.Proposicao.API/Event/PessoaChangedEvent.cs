namespace Votus.Proposicao.API.Event
{
    public class PessoaChangedEvent 
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
