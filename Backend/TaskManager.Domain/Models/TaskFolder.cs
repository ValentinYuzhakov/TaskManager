using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Models
{
    public class TaskFolder : Entity
    {
        public string Name { get; set; }
        public IList<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public Guid FolderId { get; set; }
        public TaskFolder Folder { get; set; }
        public IList<TaskFolder> TaskFolders { get; set; } = new List<TaskFolder>();
        public FolderType FolderType { get; set; } = FolderType.Default;
    }
}
