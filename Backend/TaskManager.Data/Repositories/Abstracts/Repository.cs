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

        public async Task DeleteAllAsync(IEnumerable<Guid> entityIds)
        {
            var entities = await GetQuery().Where(e => entityIds.Contains(e.Id)).ToListAsync();
            dbSet.RemoveRange(entities);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => dbSet.Update(entity));
            await SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Guid id, string include = null)
        {
            return await GetQuery(include).Where(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string include = null)
        {
            return await GetQuery(include).Where(expression).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(string include)
        {
            return await GetQuery(include).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, string include = null)
        {
            return await GetQuery(include).Where(expression).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        private IQueryable<TEntity> GetQuery(string include = null)
        {
            var query = dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(include))
            {
                var includes = include.Split('.');

                foreach (var stringInclude in includes)
                {
                    query = query.Include(stringInclude);
                }
            }

            return query;
        }
    }
}
