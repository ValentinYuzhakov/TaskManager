using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.ShortViewModels;
using TaskManager.Shared.ViewModels;

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


        public async Task CreateFolder(CreateTaskFolderInfo info)
        {
            var taskFolder = mapper.Map<TaskFolder>(info);
            await taskFolderRepository.CreateAsync(taskFolder);
        }

        public async Task UpdateFolder(UpdateTaskFolderInfo info)
        {
            var taskFolder = await taskFolderRepository.GetAsync(info.Id);
            var updatedTaskFolder = mapper.Map(info, taskFolder);
            await taskFolderRepository.UpdateAsync(updatedTaskFolder);
        }

        public async Task DeleteFolder(Guid folderId)
        {
            var folder = await taskFolderRepository.GetAsync(folderId);
            await taskFolderRepository.DeleteAsync(folder);
        }

        public async Task<List<TaskFolderShortView>> GetFoldersByUser(Guid userId)
        {
            var taskFolders = await taskFolderRepository.GetAllAsync(u => u.CreatorId == userId);
            return mapper.Map<List<TaskFolderShortView>>(taskFolders);
        }

        public async Task<TaskFolder> GetFolderById(Guid folderId)
        {
            return await taskFolderRepository.GetAsync(folderId);
        }
    }
}
