using System.Collections.Generic;
using Votus.Domain.Entities;

namespace Votus.Domain.Services
{
    public interface IPostService
    {
        void CreatePost(Post post);
        List<Post> RetrieveAllPosts();
    }
}