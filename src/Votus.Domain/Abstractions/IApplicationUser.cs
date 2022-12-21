using System;

namespace Votus.Domain.Abstractions
{
    public interface IApplicationUser : IDomainEntity
    {
        string Id { get; set; }

        string FullName { get; set; }

        string UserName { get; set; }

        string Email { get; set; }

        DateTime BirthDate { get; set; }

        

    }
}
