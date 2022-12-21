using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votus.Application.Services;
using Votus.Domain.Entities;
using Votus.Domain.Services;

namespace Votus.Web.Controllers
{
    public class PropositionController : Controller
    {
        private readonly IPropositionService _propositionService;

        public PropositionController(IPropositionService propositionService)
        {
            _propositionService = propositionService ?? throw new ArgumentNullException(nameof(propositionService));
        }

        // GET: PropositionController
        public ActionResult Index()
        {
            IEnumerable<Proposition> propositions = _propositionService.GetAllPropositions();
            
            var models = propositions.Select(x => new PropositionViewModel()
            {
                Description = x.Description,
                Theme = x.Theme,
                Question = x.Question
            });

            return View(models);
        }

        // POST: PropositionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PropositionViewModel collection)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index), ModelState.Values);

            _propositionService.CreateOrUpdateProposition(new Proposition()
            {
                Question = collection.Question,
                Description = collection.Description,
                Theme = collection.Theme
            });

            return RedirectToAction(nameof(Index));

        }

    }

    public class PropositionViewModel
    {
        public string Question { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
    }
}
