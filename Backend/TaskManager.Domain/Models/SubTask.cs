using System;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Domain.Models
{
    public class SubTask : Entity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EndDate { get; set; }
        public ToDoTaskStatus Status { get; set; }
        public Guid ToDoTaskId { get; set; }
        public ToDoTask ToDoTask { get; set; }
    }
}
