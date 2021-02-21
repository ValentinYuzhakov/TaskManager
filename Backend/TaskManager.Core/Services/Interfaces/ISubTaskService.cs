using System;
using System.Threading.Tasks;
using TaskManager.Shared.Infos.SubTasks;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ISubTaskService
    {
        Task Create(SubTaskCreateInfo info);
        Task Delete(Guid subTaskId);
        Task Update(UpdateSubTaskInfo info);
        Task UpdateStatus(UpdateSubTaskStatusInfo info);
    }
}
