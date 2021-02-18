using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAllAsync(IEnumerable<Guid> entityIds);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid id, string include = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, string include = null);
        Task<IReadOnlyList<TEntity>> GetAllAsync(string include);
        Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, string include = null);
        Task SaveChangesAsync();
    }
}
