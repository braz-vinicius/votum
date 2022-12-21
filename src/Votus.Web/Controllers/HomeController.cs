using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Votus.Domain.Entities;
using Votus.Domain.Services;
using Votus.Web.ApiClients;
using Votus.Web.Models;

namespace Votus.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostService _postService;
        private readonly IPropositionService _propositionService;

        public HomeController(ILogger<HomeController> logger, IPostService postService, IPropositionService propositionService)
        {

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _postService = postService ?? throw new ArgumentNullException(nameof(postService));

            _propositionService = propositionService ?? throw new ArgumentNullException(nameof(propositionService));
        }

        public IActionResult Index()
        {
            var posts = _postService.RetrieveAllPosts().Select(x => new PostViewModel()
            {
                Id = x.Id.ToString(),
                AuthorName = x.Author.FullName,
                AuthorId = x.AuthorId,
                CreatedAt = x.CreatedAt,
                Text = x.Text
            });

            var propositions = _propositionService.GetAllPropositions().Select(x=>new PropositionViewModel()
            {
                Question = x.Question,
                Theme = x.Theme,
                Description = x.Description
            });

            var model = new IndexViewModel()
            {
                Posts = posts,
                Propositions = propositions
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
