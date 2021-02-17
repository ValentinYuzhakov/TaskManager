using AutoMapper;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.Infos.ToDoTasks;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateTodoTaskInfo, ToDoTask>();
            CreateMap<CreateTaskFolderInfo, TaskFolder>();
            CreateMap<ToDoTask, ToDoTaskView>()
                .ForMember(t => t.CreationDate, p => p.MapFrom(r => r.CreationDate.ToString("f")))
                .ForMember(t => t.ModificationDate, p => p.MapFrom(r => r.ModificationDate.ToString("f")))
                .ForMember(t => t.EndDate, p => p.MapFrom(r => r.EndDate.ToString("f")));

            CreateMap<UpdateToDoTaskInfo, ToDoTask>()
                .ForMember(t => t.Id, o => o.Ignore());
        }
    }
}
