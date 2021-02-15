using System;

namespace TaskManager.Shared.Infos
{
    public class UpdateToDoTaskStatusInfo
    {
        public Guid Id { get; set; }
        public string TaskStatus { get; set; }
    }
}