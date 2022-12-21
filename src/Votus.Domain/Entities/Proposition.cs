using System;
using System.Collections.Generic;
using System.Text;
using Votus.Domain.Abstractions;

namespace Votus.Domain.Entities
{
    public class Proposition : IDomainEntity
    {
        public Guid Id { get; set; }

        public string Theme { get; set; }

        public string Question { get; set; }

        public string Description { get; set; }
    }
}
