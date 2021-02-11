using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TaskManager.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public IList<ToDoTask> Tasks { get; set; } = new List<ToDoTask>();
        public IList<TaskFolder> TaskFolders { get; set; } = new List<TaskFolder>();
    }
}
