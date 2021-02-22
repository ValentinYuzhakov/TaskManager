using AutoMapper;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.SubTasks;

namespace TaskManager.Core.Services
{
    public class SubTaskService : ISubTaskService
    {
        private readonly IMapper mapper;
        private readonly ISubTaskRepository subTaskRepository;


        public SubTaskService(IMapper mapper,
            ISubTaskRepository subTaskRepository)
        {
            this.mapper = mapper;
            this.subTaskRepository = subTaskRepository;
        }


        public async Task<Guid> Create(SubTaskCreateInfo info)
        {
            var subTask = mapper.Map<SubTask>(info);
            await subTaskRepository.CreateAsync(subTask);
            return subTask.Id;
        }

        public async Task Delete(Guid subTaskId)
        {
            await subTaskRepository.DeleteAsync(subTaskId);
        }

        public async Task Update(UpdateSubTaskInfo info)
        {
            var subTasktoUpdate = await subTaskRepository.GetAsync(info.SubTaskId);
            var subTask = mapper.Map(info, subTasktoUpdate);
            await subTaskRepository.UpdateAsync(subTask);
        }

        public async Task UpdateStatus(UpdateSubTaskStatusInfo info)
        {
            var subTask = await subTaskRepository.GetAsync(info.SubTaskId);
            subTask.Status = Enum.Parse<ToDoTaskStatus>(info.SubTaskStatus);
            await subTaskRepository.UpdateAsync(subTask);
        }
    }
}
