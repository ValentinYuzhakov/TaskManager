using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories
{
    public class TaskFolderRepository : IRepository<TaskFolder>
    {

        public TaskFolderRepository()
        {

        }

        public Task Create(TaskFolder entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TaskFolder entity)
        {
            throw new NotImplementedException();
        }

        public Task<TaskFolder> Get(Guid entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskFolder entity)
        {
            throw new NotImplementedException();
        }
    }
}
