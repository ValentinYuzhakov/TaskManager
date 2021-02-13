using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class User : IdentityUser<Guid>, IEntity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public IList<ToDoTask> Tasks { get; } = new List<ToDoTask>();
        public IList<TaskFolder> TaskFolders { get; set; } = new List<TaskFolder>();
    }
}
