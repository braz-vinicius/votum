using System;
using System.Collections.Generic;
using System.Text;
using Votus.Domain.Abstractions;

namespace Votus.Domain.Entities
{
    public class Vote : IDomainEntity
    {
        public Guid Id { get; set; }

        public virtual Person Person { get; set; }

        public virtual Proposition Proposition { get; set; }

        public VoteValueType Value { get; set; }
        
        public Guid PersonId { get; set; }

        public string PropositionId { get; set; }
    }
}