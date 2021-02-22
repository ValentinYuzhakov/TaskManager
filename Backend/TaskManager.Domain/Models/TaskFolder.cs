using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Domain.JoinTables;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class TaskFolder : Entity
    {
        public string Name { get; set; }
        public Guid? CreatorId { get; set; }
        public User Creator { get; set; }
        public FolderType Type { get; set; }
        public IList<TaskFolderTodoTask> TasksInFolder { get; set; } = new List<TaskFolderTodoTask>();
    }
}
