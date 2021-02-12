using System;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetAsync(Guid entity);
        Task UpdateAsync(T entity);
    }
}
