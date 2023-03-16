using System;
using System.Collections.Generic;
using System.Text;
using Votus.Common.Domain;

namespace Votus.Voto.API.Domain
{
    public class Proposicao : IDomainEntity
    {
        public Proposicao(Guid id, string tema, string questao, string descricao)
        {
            Id = id;
            Tema = tema;
            Questao = questao;
            Descricao = descricao;
        }

        public Guid Id { get; set; }

        public string Tema { get; set; }

        public string Questao { get; set; }

        public string Descricao { get; set; }
    }
}
