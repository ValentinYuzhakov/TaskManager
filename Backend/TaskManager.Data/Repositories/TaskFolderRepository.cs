using System;
using System.Threading.Tasks;
using TaskManager.Data.DataContext.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskFolderRepository : ITaskFolderRepository
    {
        private readonly IDataContext<TaskFolder> context;


        public TaskFolderRepository(IDataContext<TaskFolder> context)
        {
            this.context = context;
        }


        public async Task CreateAsync(TaskFolder entity)
        {
            await context.CreateAsync(entity);
        }

        public async Task DeleteAsync(TaskFolder entity)
        {
            await context.DeleteAsync(entity);
        }

        public async Task<TaskFolder> GetAsync(Guid entityId)
        {
            return await context.GetAsync(entityId);
        }

        public async Task UpdateAsync(TaskFolder entity)
        {
            await context.UpdateAsync(entity);
        }
    }
}
