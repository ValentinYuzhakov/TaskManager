using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Abstracts;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskFolderRepository : Repository<TaskFolder>, ITaskFolderRepository
    {
        public TaskFolderRepository(DatabaseContext context) : base(context) { }

        public async Task CreateRangeAsync(IEnumerable<TaskFolder> taskFolders)
        {
            await dbSet.AddRangeAsync(taskFolders);
            await SaveChangesAsync();
        }
    }
}
