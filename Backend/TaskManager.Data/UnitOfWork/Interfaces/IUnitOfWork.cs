using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Data.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public ISubTaskRepository SubTaskRepository { get; }
        public IUserRepository UserRepository { get; }
        public IToDoTaskRepository ToDoTaskRepository { get; }
        public ITaskFolderRepository TaskFolderRepository { get; }

        Task SaveChangesAsync();
    }
}
