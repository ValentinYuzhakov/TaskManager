using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Shared.Infos;

namespace TaskManager.Data.Services.Interfaces
{
    public interface ITodoTaskService
    {
        Task CreateToDoTask(CreateTodoTaskInfo taskInfo);
    }
}
