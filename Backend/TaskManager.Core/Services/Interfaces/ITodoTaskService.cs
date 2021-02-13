using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Data.Services.Interfaces
{
    public interface ITodoTaskService
    {
        Task CreateToDoTask(CreateTodoTaskInfo taskInfo);
        Task<ToDoTaskView> GetById(Guid taskId);
        Task<List<ToDoTaskView>> GetTasksByUser(Guid userId);
    }
}
