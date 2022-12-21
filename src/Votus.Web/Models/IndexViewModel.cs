using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votus.Domain.Entities;
using Votus.Web.Controllers;

namespace Votus.Web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }

        public IEnumerable<PropositionViewModel> Propositions { get; set; }
    }
}
