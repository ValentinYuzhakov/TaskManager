using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class TaskFolder : Entity
    {
        public string Name { get; set; }
        public IList<TaskFolderTodoTask> TasksInFolder { get; set; } = new List<TaskFolderTodoTask>();
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public FolderType FolderType { get; set; } = FolderType.Default;
    }
}
