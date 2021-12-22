using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface ITaskFolderRepository : IRepository<TaskFolder>
    {
        Task CreateRangeAsync(IEnumerable<TaskFolder> taskFolders);
        Task<IReadOnlyList<TaskFolder>> GetSystemFolders();
    }
}
