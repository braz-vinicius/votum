using System;
using System.Collections.Generic;
using System.Text;
using Votus.Common.Domain;

namespace Votus.Voto.API.Domain
{
    public class Voto : IDomainEntity
    {
        public Guid Id { get; set; }

        public virtual string PessoaNome { get; set; }

        public virtual string ProposicaoNome { get; set; }

        public VotoValueType Value { get; set; }

        public virtual Guid PessoaId { get; set; }

        public virtual string ProposicaoId { get; set; }
    }
}