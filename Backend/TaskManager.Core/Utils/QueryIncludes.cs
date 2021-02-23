using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.JoinTables;
using TaskManager.Domain.Models;

namespace TaskManager.Core.Utils
{
    public class QueryIncludes
    {
        public static string TaskFolderWithTasks = $"{nameof(TaskFolder.TasksInFolder)},{nameof(TaskFolderTodoTask.ToDoTask)}";
    }
}
