using TaskManager.Data.Repositories.Abstracts;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class ToDoTaskRepository : Repository<ToDoTask>, IToDoTaskRepository
    {
        public ToDoTaskRepository(DatabaseContext context) : base(context) { }
    }
}
