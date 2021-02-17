using System;

namespace TaskManager.Shared.Infos.ToDoTasks
{
    public class CreateTodoTaskInfo
    {
        public Guid? FolderId { get; set; }
        public string Title { get; set; }
        public Guid CreatorId { get; set; }
    }
}
