using System;
using System.Threading.Tasks;
using TaskManager.Domain.Models.Abstracts;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : IEntity<Guid>
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetAsync(Guid entityId);
        Task UpdateAsync(T entity);
    }
}
