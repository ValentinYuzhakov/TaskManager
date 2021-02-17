using System;
using TaskManager.Domain.Enums;

namespace TaskManager.Shared.Infos.ToDoTasks
{
    public class UpdateToDoTaskInfo
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string EndDate { get; set; }
    }
}
