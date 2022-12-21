using System;
using System.Collections.Generic;
using System.Text;
using Votus.Domain.Abstractions;

namespace Votus.Domain.Entities
{
    public class Post : IDomainEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public virtual Person Author { get; set; }

        public string AuthorId { get; set; }

    }
}
