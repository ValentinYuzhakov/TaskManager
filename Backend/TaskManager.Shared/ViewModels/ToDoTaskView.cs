using System;

namespace TaskManager.Shared.ViewModels
{
    public class ToDoTaskView
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string CreationDate { get; set; }
        public string ModificationDate { get; set; }
        public string EndDate { get; set; }
        public string TaskPriority { get; set; }
        public string TaskStatus { get; set; }
    }
}
