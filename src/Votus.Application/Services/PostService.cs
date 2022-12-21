using System;
using System.Collections.Generic;
using System.Linq;
using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Domain.Services;

namespace Votus.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;

        public PostService(IPostRepository repository)
        {
            _repository = repository;
        }

        public void CreatePost(Post post)
        {
            post.CreatedAt = DateTime.UtcNow;

            _repository.Add(post);
            
        }

        public List<Post> RetrieveAllPosts()
        {
            return _repository.GetAll().OrderByDescending(x=>x.CreatedAt).ToList();
        }

    }
}