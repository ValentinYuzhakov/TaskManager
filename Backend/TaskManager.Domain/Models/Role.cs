using Microsoft.AspNetCore.Identity;
using System;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class Role : IdentityRole<Guid>, IEntity<Guid>
    {
    }
}
