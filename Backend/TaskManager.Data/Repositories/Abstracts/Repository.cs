using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Data.Repositories.Abstracts
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly DatabaseContext context;
        protected readonly DbSet<TEntity> dbSet;


        public Repository(DatabaseContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }


        public async Task CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Remove(entity));
            await SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, string unclude = null)
        {
            return await dbSet.Where(expression).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Guid entityId)
        {
            return await dbSet.FindAsync(entityId);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Update(entity));
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
