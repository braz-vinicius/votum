using System;
using Microsoft.AspNetCore.Identity;
using Votus.Domain.Abstractions;

namespace Votus.Infrastructure.Identity
{
    public class WebApplicationUser : IdentityUser, IApplicationUser
    {
        public DateTime BirthDate { get; set; }

        public string FullName { get; set; }

        public string ProfileImageUrl { get; set; }

        
    }
}
