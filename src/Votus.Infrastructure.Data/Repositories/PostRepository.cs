using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Votus.Domain.Entities;
using Votus.Domain.Repositories;
using Votus.Infrastructure.Data.Providers;

namespace Votus.Infrastructure.Data.Repositories
{
    public class PostRepository : SqlRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
