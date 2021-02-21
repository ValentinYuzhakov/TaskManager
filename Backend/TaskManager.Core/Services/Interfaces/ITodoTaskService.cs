using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Shared.Infos.ToDoTasks;
using TaskManager.Shared.ShortViewModels;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ITodoTaskService
    {
        Task Create(CreateTodoTaskInfo taskInfo);
        Task<ToDoTaskView> GetById(Guid taskId);
        Task Delete(Guid taskId);
        Task<List<ToDoTaskShortView>> GetAllByUser(Guid userId);
        Task Update(UpdateToDoTaskInfo taskinfo);
        Task UpdatePriority(UpdateToDoTaskPriorityInfo taskInfo);
        Task UpdateStatus(UpdateToDoTaskStatusInfo taskInfo);
        Task<List<ToDoTaskShortView>> GetDoneTasks(Guid userId);
        Task<List<ToDoTaskShortView>> GetImportantTasks(Guid userId);
        Task<List<ToDoTaskShortView>> GetDailyTasks(Guid userId);
        Task<List<ToDoTaskShortView>> GetUserTasksByFolder(Guid folderId, Guid userId);
        Task MoveToFolder(Guid taskId, Guid folderId);
        Task<List<ToDoTaskShortView>> GetPlannedTasks(Guid userId);
    }
}
