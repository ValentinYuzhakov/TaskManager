using System;
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
        }

        public async Task DeleteAsync(ToDoTask entity)
        {
            await Task.Run(() => context.Remove(entity));
        }

        public async Task<ToDoTask> GetAsync(Guid entityId)
        {
            return await context.Tasks.FindAsync(entityId);
        }

        public async Task UpdateAsync(ToDoTask entity)
        {
            await Task.Run(() => context.Update(entity));
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
