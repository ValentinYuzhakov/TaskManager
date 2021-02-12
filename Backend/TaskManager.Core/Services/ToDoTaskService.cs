using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Data.Services.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Services
{
    public class ToDoTaskService : ITodoTaskService
    {
        private readonly IToDoTaskRepository repository;


        public ToDoTaskService(IToDoTaskRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateToDoTask(ToDoTask toDoTask)
        {
            await repository.CreateAsync(toDoTask);
        }


    }
}
