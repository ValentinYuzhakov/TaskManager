using System;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task Create(T entity);
        void Delete(T entity);
        Task<T> Get(Guid entity);
        void Update(T entity);
    }
}
