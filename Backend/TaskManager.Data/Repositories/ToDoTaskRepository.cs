using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly DatabaseContext context;


        public ToDoTaskRepository(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task CreateAsync(ToDoTask entity)
        {
            await context.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(ToDoTask entity)
        {
            await Task.Run(() => context.Remove(entity));
            await SaveChangesAsync();
        }

        public async Task<ToDoTask> GetAsync(Guid entityId)
        {
            return await context.Tasks.FindAsync(entityId);
        }

        public async Task UpdateAsync(ToDoTask entity)
        {
            await Task.Run(() => context.Tasks.Update(entity));
            await SaveChangesAsync();
        }

        public async Task<List<ToDoTask>> GetAllAsync()
        {
            return await Task.Run(() => context.Tasks.ToList());
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<List<ToDoTask>> GetAllAsync(Func<ToDoTask, bool> func)
        {
            return await Task.Run(() => context.Tasks.Where(func).ToList());
        }
    }
}
