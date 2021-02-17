using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ITaskFolderService
    {
        Task CreateFolder(CreateTaskFolderInfo info);
        Task UpdateFolder(UpdateTaskFolderInfo info);
        Task DeleteFolder(Guid folderId);
        Task<List<TaskFolderView>> GetFoldersByUser(Guid userId);
    }
}
