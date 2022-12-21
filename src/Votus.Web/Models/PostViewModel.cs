using System;

namespace Votus.Web.Models
{
    public record PostViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string AuthorName { get; set; }

        public string AuthorId { get; set; }
    }
}