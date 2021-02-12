using System;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Data.DataContext.Interfaces
{
    public interface IDataContext<TEntity> where TEntity : Entity
    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid entityId);
        Task UpdateAsync(TEntity entity);
    }
}
