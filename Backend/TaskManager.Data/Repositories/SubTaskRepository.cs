using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class SubTaskRepository : IRepository<SubTask>
    {

        public SubTaskRepository()
        {

        }

        public Task Create(SubTask entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(SubTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<SubTask> Get(Guid entity)
        {
            throw new NotImplementedException();
        }

        public void Update(SubTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
