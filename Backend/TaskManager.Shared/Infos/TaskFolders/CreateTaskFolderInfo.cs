using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Shared.Infos.TaskFolders
{
    public class CreateTaskFolderInfo
    {
        public Guid CreatorId { get; set; }
        public string Name { get; set; }
    }
}
