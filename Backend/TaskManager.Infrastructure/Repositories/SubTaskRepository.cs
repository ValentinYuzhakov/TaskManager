using TaskManager.Data.Repositories.Abstracts;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class SubTaskRepository : Repository<SubTask>, ISubTaskRepository
    {
        public SubTaskRepository(DatabaseContext context) : base(context) { }
    }
}
