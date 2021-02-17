using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid entityId);
        Task UpdateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(Func<TEntity, bool> func);
        Task SaveChangesAsync();
    }
}
