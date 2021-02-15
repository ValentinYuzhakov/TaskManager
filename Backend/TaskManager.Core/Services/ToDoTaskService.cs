using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Data.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Data.Services
{
    public class ToDoTaskService : ITodoTaskService
    {
        private readonly IMapper mapper;
        private readonly IToDoTaskRepository repository;


        public ToDoTaskService(IToDoTaskRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task CreateToDoTask(CreateTodoTaskInfo taskInfo)
        {
            var task = mapper.Map<ToDoTask>(taskInfo);
            await repository.CreateAsync(task);
            await repository.SaveChangesAsync();
        }

        public async Task<ToDoTaskView> GetById(Guid taskId)
        {
            var task = await repository.GetAsync(taskId);
            var taskView = mapper.Map<ToDoTaskView>(task);
            return taskView;
        }

        public async Task<List<ToDoTaskView>> GetTasksByUser(Guid userId)
        {
            var tasks = await repository.GetAllAsync();
            var userTasks = tasks.Where(t => t.CreatorId == userId).ToList();
            return mapper.Map<List<ToDoTaskView>>(userTasks);
        }

        public async Task UpdateToDoTask(UpdateToDoTaskInfo taskinfo)
        {

        }
    }
}
