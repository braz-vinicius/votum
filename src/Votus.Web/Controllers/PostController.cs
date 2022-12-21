using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Votus.Domain.Entities;
using Votus.Domain.Services;
using Votus.Infrastructure.Identity;
using Votus.Web.ApiClients;
using Votus.Web.Models;

namespace Votus.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<WebApplicationUser> _userManager;


        public PostController(IPostService apiClient, UserManager<WebApplicationUser> userManager)
        {
            _postService = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostViewModel request)
        {

            var appUser = await _userManager.FindByNameAsync(User.Identity?.Name);

            _postService.CreatePost(new Post()
            {
                AuthorId = appUser.Id,
                Text = request.Text,
            });

            return RedirectToAction("Index", "Home");
        }

      
    }
}
