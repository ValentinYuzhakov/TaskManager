using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class ToDoTaskRepository : IRepository<ToDoTask>
    {
        private readonly DatabaseContext dbContext;


        public ToDoTaskRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task Create(ToDoTask entity)
        {
            await dbContext.AddAsync(entity);
        }

        public void Delete(ToDoTask entity)
        {
            dbContext.Remove(entity);
        }

        public async Task<ToDoTask> Get(Guid entityId)
        {
            return await dbContext.Tasks.FindAsync(entityId);
        }

        public void Update(ToDoTask entity)
        {
            dbContext.Update(entity);
        }
    }
}
