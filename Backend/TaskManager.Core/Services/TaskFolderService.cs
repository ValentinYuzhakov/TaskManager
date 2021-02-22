using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.ShortViewModels;

namespace TaskManager.Core.Services
{
    public class TaskFolderService : ITaskFolderService
    {
        private readonly IMapper mapper;
        private readonly ITaskFolderRepository taskFolderRepository;


        public TaskFolderService(IMapper mapper,
            ITaskFolderRepository taskFolderRepository)
        {
            this.mapper = mapper;
            this.taskFolderRepository = taskFolderRepository;
        }


        public async Task<Guid> Create(CreateTaskFolderInfo info)
        {
            var taskFolder = mapper.Map<TaskFolder>(info);
            await taskFolderRepository.CreateAsync(taskFolder);
            return taskFolder.Id;
        }

        public async Task Update(UpdateTaskFolderInfo info)
        {
            var taskFolder = await taskFolderRepository.GetAsync(info.Id);
            var updatedTaskFolder = mapper.Map(info, taskFolder);
            await taskFolderRepository.UpdateAsync(updatedTaskFolder);
        }

        public async Task Delete(Guid folderId)
        {
            var folder = await taskFolderRepository.GetAsync(folderId);
            await taskFolderRepository.DeleteAsync(folder);
        }

        public async Task<IReadOnlyList<TaskFolderShortView>> GetByUser(Guid userId)
        {
            var taskFolders = await taskFolderRepository.GetAllAsync(u => u.CreatorId == userId);
            return mapper.Map<List<TaskFolderShortView>>(taskFolders);
        }

        public async Task<TaskFolder> GetById(Guid folderId)
        {
            return await taskFolderRepository.GetAsync(folderId);
        }

        public async Task<TaskFolder> GetSystemFolder(FolderType folderType)
        {
            return await taskFolderRepository.GetAsync(f => f.Type == folderType);
        }
    }
}
