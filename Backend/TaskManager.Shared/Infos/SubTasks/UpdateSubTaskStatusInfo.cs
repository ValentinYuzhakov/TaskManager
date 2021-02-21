using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Shared.Infos.SubTasks
{
    public class UpdateSubTaskStatusInfo
    {
        public Guid SubTaskId { get; set; }
        public string SubTaskStatus { get; set; }
    }
}
