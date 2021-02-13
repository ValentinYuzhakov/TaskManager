using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Data.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateTodoTaskInfo, ToDoTask>();
            CreateMap<ToDoTask, ToDoTaskView>()
                .ForMember(t => t.CreationDate, p => p.MapFrom(r => r.CreationDate.ToString("f")))
                .ForMember(t => t.ModificationDate, p => p.MapFrom(r => r.ModificationDate.ToString("f")))
                .ForMember(t => t.EndDate, p => p.MapFrom(r => r.EndDate.ToString("f")));
        }
    }
}
