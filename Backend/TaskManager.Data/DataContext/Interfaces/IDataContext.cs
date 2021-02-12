using System;
using System.Threading.Tasks;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Data.DataContext.Interfaces
{
    public interface IDataContext<TEntity> where TEntity : class, IEntity<Guid>
    {
        Task CreateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetAsync(Guid entityId);
        Task UpdateAsync(TEntity entity);
        Task SaveChangesAsync();
    }
}
