using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskFolderRepository : ITaskFolderRepository
    {
        private readonly DatabaseContext context;


        public TaskFolderRepository(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task CreateAsync(TaskFolder entity)
        {
            await context.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TaskFolder entity)
        {
            await Task.Run(() => context.Remove(entity));
            await SaveChangesAsync();
        }

        public async Task<TaskFolder> GetAsync(Guid entityId)
        {
            return await context.TaskFolders.FindAsync(entityId);
        }

        public async Task UpdateAsync(TaskFolder entity)
        {
            await Task.Run(() => context.Update(entity));
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<TaskFolder>> GetAllAsync(Func<TaskFolder, bool> func)
        {
            return await Task.Run(() => context.TaskFolders.Where(func).ToList());
        }
    }
}
