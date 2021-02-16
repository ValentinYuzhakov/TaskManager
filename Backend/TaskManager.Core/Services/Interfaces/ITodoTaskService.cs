using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ITodoTaskService
    {
        Task CreateToDoTask(CreateTodoTaskInfo taskInfo);
        Task<ToDoTaskView> GetById(Guid taskId);
        Task<List<ToDoTaskView>> GetTasksByUser(Guid userId);
        Task UpdateToDoTask(UpdateToDoTaskInfo taskinfo);
        Task UpdatePriority(UpdateToDoTaskPriorityInfo taskInfo);
        Task UpdateStatus(UpdateToDoTaskStatusInfo taskInfo);
        Task<List<ToDoTaskView>> GetDoneTasks(Guid userId);
        Task<List<ToDoTaskView>> GetImportantTasks(Guid userId);
        Task<List<ToDoTaskView>> GetDailyTasks(Guid userId);
    }
}
