using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class TaskFolder : Entity
    {
        public string Name { get; set; }
        public IList<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public Guid ParentFolderId { get; set; }
        public TaskFolder ParentFolder { get; set; }
        public IList<TaskFolder> Folders { get; set; }
        public FolderType FolderType { get; set; } = FolderType.Default;
    }
}
