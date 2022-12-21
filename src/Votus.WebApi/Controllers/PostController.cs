using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votus.Domain.Entities;
using Votus.Domain.Services;

namespace Votus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return _postService.RetrieveAllPosts();
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Post value)
        {
            _postService.CreatePost(value);
        }

    }
}
