using System;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class SubTask : Entity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ModificationDate { get; set; }
        public DateTime EndDate { get; set; }
        public TaskStatus TaskStatus { get; set; } = TaskStatus.InProgress;
        public Guid ToDoTaskId { get; set; }
        public ToDoTask ToDoTask { get; set; }
    }
}
