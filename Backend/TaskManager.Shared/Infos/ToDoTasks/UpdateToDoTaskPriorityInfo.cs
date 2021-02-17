using System;

namespace TaskManager.Shared.Infos.ToDoTasks
{
    public class UpdateToDoTaskPriorityInfo
    {
        public Guid Id { get; set; }
        public string TaskPriority { get; set; }
    }
}
