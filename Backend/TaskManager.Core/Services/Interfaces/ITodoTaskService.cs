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
        Task<Guid> Create(CreateTodoTaskInfo taskInfo);
        Task<ToDoTaskView> GetById(Guid taskId);
        Task Delete(Guid taskId);
        Task<IReadOnlyList<ToDoTaskShortView>> GetAllByUser(Guid userId);
        Task Update(UpdateToDoTaskInfo taskinfo);
        Task UpdatePriority(UpdateToDoTaskPriorityInfo taskInfo);
        Task UpdateStatus(UpdateToDoTaskStatusInfo taskInfo);
        Task<IReadOnlyList<ToDoTaskShortView>> GetDoneTasks(Guid userId);
        Task<IReadOnlyList<ToDoTaskShortView>> GetImportantTasks(Guid userId);
        Task<IReadOnlyList<ToDoTaskShortView>> GetDailyTasks(Guid userId);
        Task<IReadOnlyList<ToDoTaskShortView>> GetUserTasksByFolder(Guid folderId, Guid userId);
        Task MoveToFolder(Guid taskId, Guid folderId);
        Task<IReadOnlyList<ToDoTaskShortView>> GetPlannedTasks(Guid userId);
    }
}
