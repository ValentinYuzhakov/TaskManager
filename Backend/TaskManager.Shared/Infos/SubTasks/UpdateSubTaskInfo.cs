using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Shared.Infos.SubTasks
{
    public class UpdateSubTaskInfo
    {
        public Guid SubTaskId { get; set; }
        public string Name { get; set; }
        public string EndDate { get; set; }
    }
}
