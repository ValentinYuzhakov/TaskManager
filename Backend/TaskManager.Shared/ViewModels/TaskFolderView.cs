using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Shared.ViewModels
{
    public class TaskFolderView
    {
        public string Name { get; set; }
        public int TotalTasks { get; set; }
        public int DoneTasks { get; set; }
    }
}
