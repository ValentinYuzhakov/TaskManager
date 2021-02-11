using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TaskManager.Data.DataContext.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.DataContext
{
    public class SqlContext<TEntity> : IDataContext<TEntity> where TEntity : Entity
    {
        private readonly DbSet<TEntity> dbSet;


        public SqlContext(DatabaseContext context)
        {
            dbSet = context.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<TEntity> GetAsync(Guid entityId)
        {
            return await dbSet.FindAsync(entityId);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Update(entity));
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Remove(entity));
        }
    }
}
