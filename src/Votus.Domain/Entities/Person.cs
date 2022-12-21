using System;
using System.Collections.Generic;
using System.Text;
using Votus.Domain.Abstractions;

namespace Votus.Domain.Entities
{
    public class Person : IApplicationUser
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Proposition> Propositions { get; set; }

        public virtual List<Vote> Votes { get; set; }
    }
}
