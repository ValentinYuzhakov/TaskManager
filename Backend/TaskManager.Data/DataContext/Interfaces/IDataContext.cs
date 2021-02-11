using System;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Data.DataContext.Interfaces
{
    public interface IDataContext<TEntity> where TEntity : Entity
    {
        public Task CreateAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
        public Task<TEntity> GetAsync(Guid entityId);
        public Task UpdateAsync(TEntity entity);
    }
}
