using System;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        //private readonly IDataContext<SubTask> context;
        private readonly DatabaseContext context;


        public SubTaskRepository(DatabaseContext context)
        {
            this.context = context;
        }


        public async Task CreateAsync(SubTask entity)
        {
            await context.AddAsync(entity);
        }

        public async Task DeleteAsync(SubTask entity)
        {
            await Task.Run(() => context.Remove(entity));
        }

        public async Task<SubTask> GetAsync(Guid entityId)
        {
            return await context.SubTasks.FindAsync(entityId);
        }

        public async Task UpdateAsync(SubTask entity)
        {
            await Task.Run(() => context.Update(entity));
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
