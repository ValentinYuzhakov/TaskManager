using System;
using System.Threading.Tasks;
using TaskManager.Data.DataContext.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly IDataContext<SubTask> context;


        public SubTaskRepository(IDataContext<SubTask> context)
        {
            this.context = context;
        }


        public async Task CreateAsync(SubTask entity)
        {
            await context.CreateAsync(entity);
        }

        public async Task DeleteAsync(SubTask entity)
        {
            await context.DeleteAsync(entity);
        }

        public async Task<SubTask> GetAsync(Guid entityId)
        {
            return await context.GetAsync(entityId);
        }

        public async Task UpdateAsync(SubTask entity)
        {
            await context.UpdateAsync(entity);
        }
    }
}
