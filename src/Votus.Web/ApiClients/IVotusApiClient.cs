using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Votus.Domain.Entities;
using Votus.Web.Models;

namespace Votus.Web.ApiClients
{
    public interface IVotusApiClient
    {
        [Put("/api/Post")]
        Task CreatePost(PostRequest request);

        [Get("/api/Post")]
        Task<IEnumerable<Post>> GetAllPosts();
    }
}
