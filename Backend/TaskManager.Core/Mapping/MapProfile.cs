using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;

namespace TaskManager.Data.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreateTodoTaskInfo, ToDoTask>();
        }
    }
}
