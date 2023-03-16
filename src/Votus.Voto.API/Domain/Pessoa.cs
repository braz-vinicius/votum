using System;
using System.Collections.Generic;
using System.Text;
using Votus.Common.Domain;

namespace Votus.Voto.API.Domain
{
    public class Pessoa : IDomainEntity
    {
        public string Id { get; set; }

        public string NomeCompleto { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }

    }
}
