using System;
using TaskManager.Domain.Models;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.JoinTables
{
    public class TaskFolderTodoTask : Entity
    {
        public Guid TaskFolderId { get; set; }
        public TaskFolder TaskFolder { get; set; }
        public Guid? ToDoTaskId { get; set; }
        public ToDoTask ToDoTask { get; set; }
    }
}
