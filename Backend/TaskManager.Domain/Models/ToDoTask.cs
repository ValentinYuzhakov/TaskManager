using System;
using System.Collections.Generic;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class ToDoTask : Entity
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; } = DateTime.Now;
        public DateTime ModificationDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskPriority TaskPriority { get; set; } = TaskPriority.None;
        public TaskStatus TaskStatus { get; set; } = TaskStatus.InProgress;
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public IList<SubTask> SubTasks { get; } = new List<SubTask>();
        public IList<TaskFolder> Folders { get; } = new List<TaskFolder>();
    }
}
