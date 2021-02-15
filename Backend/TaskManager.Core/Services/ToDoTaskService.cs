﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Extensions;
using TaskManager.Core.Repositories.Interfaces;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services
{
    public class ToDoTaskService : ITodoTaskService
    {
        private readonly IMapper mapper;
        private readonly IToDoTaskRepository repository;


        public ToDoTaskService(IToDoTaskRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        public async Task CreateToDoTask(CreateTodoTaskInfo taskInfo)
        {
            var task = mapper.Map<ToDoTask>(taskInfo);
            await repository.CreateAsync(task);
            await repository.SaveChangesAsync();
        }

        public async Task<ToDoTaskView> GetById(Guid taskId)
        {
            var task = await repository.GetAsync(taskId);
            var taskView = mapper.Map<ToDoTaskView>(task);
            return taskView;
        }

        public async Task<List<ToDoTaskView>> GetTasksByUser(Guid userId)
        {
            var tasks = await repository.GetAllAsync();
            var userTasks = tasks.Where(t => t.CreatorId == userId).ToList();
            return mapper.Map<List<ToDoTaskView>>(userTasks);
        }

        public async Task UpdateToDoTask(UpdateToDoTaskInfo taskinfo)
        {
            var taskToUpdate = await repository.GetAsync(taskinfo.Id);
            var todoTask = mapper.Map<ToDoTask>(taskinfo);

            taskToUpdate.Title = todoTask.Title ?? taskToUpdate.Title;
            taskToUpdate.Comment = todoTask.Comment ?? taskToUpdate.Comment;
            taskToUpdate.ModificationDate = DateTime.Now;

            if (!todoTask.EndDate.IsDefault())
            {
                taskToUpdate.EndDate = todoTask.EndDate;
            }

            await repository.UpdateAsync(taskToUpdate);
            await repository.SaveChangesAsync();
        }

        public async Task UpdatePriority(UpdateToDoTaskPriorityInfo taskInfo)
        {
            var task = await repository.GetAsync(taskInfo.Id);
            task.TaskPriority = Enum.Parse<TaskPriority>(taskInfo.TaskPriority);

            await repository.UpdateAsync(task);
            await repository.SaveChangesAsync();
        }

        public async Task UpdateStatus(UpdateToDoTaskStatusInfo taskInfo)
        {
            var task = await repository.GetAsync(taskInfo.Id);
            task.TaskStatus = Enum.Parse<TaskManager.Domain.Enums.TaskStatus>(taskInfo.TaskStatus);

            await repository.UpdateAsync(task);
            await repository.SaveChangesAsync();
        }
    }
}
