using AutoMapper;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;

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




    }
}
