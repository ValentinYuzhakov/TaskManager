using System;
using System.Threading.Tasks;
using TaskManager.Data.DataContext.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class ToDoTaskRepository : IToDoTaskRepository
    {
        private readonly IDataContext<ToDoTask> context;


        public ToDoTaskRepository(IDataContext<ToDoTask> context)
        {
            this.context = context;
        }


        public async Task CreateAsync(ToDoTask entity)
        {
            await context.CreateAsync(entity);
        }

        public async Task DeleteAsync(ToDoTask entity)
        {
            await context.DeleteAsync(entity);
        }

        public async Task<ToDoTask> GetAsync(Guid entityId)
        {
            return await context.GetAsync(entityId);
        }

        public async Task UpdateAsync(ToDoTask entity)
        {
            await context.UpdateAsync(entity);
        }
    }
}
