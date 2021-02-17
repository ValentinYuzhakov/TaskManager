using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Enums;

namespace TaskManager.Shared.Infos.ToDoTasks
{
    public class UpdateToDoTaskPriorityInfo
    {
        public Guid Id { get; set; }
        public string TaskPriority { get; set; }
    }
}
