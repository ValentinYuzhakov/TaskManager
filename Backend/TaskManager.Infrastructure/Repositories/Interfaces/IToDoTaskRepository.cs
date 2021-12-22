using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Models;

namespace TaskManager.Data.Repositories.Interfaces
{
    public interface IToDoTaskRepository : IRepository<ToDoTask>
    {
    }
}
