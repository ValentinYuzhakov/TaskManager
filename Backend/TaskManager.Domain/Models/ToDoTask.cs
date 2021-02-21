using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Domain.JoinTables;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class ToDoTask : Entity
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public ToDoTaskStatus TaskStatus { get; set; }
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public IList<SubTask> SubTasks { get; set; } = new List<SubTask>();
        public IList<TaskFolderTodoTask> Folders { get; set; } = new List<TaskFolderTodoTask>();
    }
}
