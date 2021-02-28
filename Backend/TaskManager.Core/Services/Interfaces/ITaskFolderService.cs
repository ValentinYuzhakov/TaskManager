using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services.Interfaces
{
    public interface ITaskFolderService
    {
        Task<Guid> Create(CreateTaskFolderInfo info);
        Task Update(UpdateTaskFolderInfo info);
        Task Delete(Guid folderId);
        Task<IReadOnlyList<TaskFolderView>> GetByUser(Guid userId);
        Task<TaskFolder> GetById(Guid folderId);
        Task<TaskFolder> GetSystemFolder(FolderType folderType);
        Task<IReadOnlyList<TaskFolder>> CreateSystemFolders();
    }
}
