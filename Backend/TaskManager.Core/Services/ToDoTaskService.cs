using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Data.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;

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


    }
}
