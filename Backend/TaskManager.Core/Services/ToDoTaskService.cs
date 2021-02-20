using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Data.Repositories.Interfaces;
using TaskManager.Domain.Enums;
using TaskManager.Domain.JoinTables;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.ToDoTasks;
using TaskManager.Shared.ShortViewModels;
using TaskManager.Shared.ViewModels;

namespace TaskManager.Core.Services
{
    public class ToDoTaskService : ITodoTaskService
    {
        private readonly IMapper mapper;
        private readonly IToDoTaskRepository repository;
        private readonly ITaskFolderService taskFolderService;


        public ToDoTaskService(IToDoTaskRepository repository, IMapper mapper,
            ITaskFolderService taskFolderService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.taskFolderService = taskFolderService;
        }


        public async Task CreateToDoTask(CreateTodoTaskInfo taskInfo)
        {
            var task = mapper.Map<ToDoTask>(taskInfo);

            if (taskInfo.FolderId.HasValue)
            {
                var folder = await taskFolderService.GetFolderById(taskInfo.FolderId.Value);

                task.Folders.Add(new TaskFolderTodoTask
                {
                    TaskFolderId = taskInfo.FolderId.Value,
                    TaskFolder = folder
                });
            }

            await repository.CreateAsync(task);
        }

        public async Task DeleteToDoTask(Guid taskId)
        {
            await repository.DeleteAsync(taskId);
        }

        public async Task<ToDoTaskView> GetById(Guid taskId)
        {
            var task = await repository.GetAsync(taskId);
            var taskView = mapper.Map<ToDoTaskView>(task);
            return taskView;
        }

        public async Task<List<ToDoTaskView>> GetTasksByUser(Guid userId)
        {
            var tasks = await repository.GetAllAsync(t => t.CreatorId == userId);
            return mapper.Map<List<ToDoTaskView>>(tasks);
        }

        public async Task UpdateToDoTask(UpdateToDoTaskInfo taskinfo)
        {
            var taskToUpdate = await repository.GetAsync(taskinfo.Id);
            var todoTask = mapper.Map(taskinfo, taskToUpdate);
            await repository.UpdateAsync(todoTask);
        }

        public async Task UpdatePriority(UpdateToDoTaskPriorityInfo taskInfo)
        {
            var task = await repository.GetAsync(taskInfo.Id);
            task.TaskPriority = Enum.Parse<TaskPriority>(taskInfo.TaskPriority);

            await repository.UpdateAsync(task);
        }

        public async Task UpdateStatus(UpdateToDoTaskStatusInfo taskInfo)
        {
            var task = await repository.GetAsync(taskInfo.Id);
            task.TaskStatus = Enum.Parse<TaskManager.Domain.Enums.TaskStatus>(taskInfo.TaskStatus);

            await repository.UpdateAsync(task);
        }

        public async Task<List<ToDoTaskView>> GetDoneTasks(Guid userId)
        {
            var tasks = await repository.GetAllAsync(t => t.CreatorId == userId &&
                t.TaskStatus == Domain.Enums.TaskStatus.Done);

            return mapper.Map<List<ToDoTaskView>>(tasks);
        }

        public async Task<List<ToDoTaskView>> GetImportantTasks(Guid userId)
        {
            var tasks = (await repository.GetAllAsync(t => t.CreatorId == userId &&
                t.TaskPriority == TaskPriority.Highest || t.TaskPriority == TaskPriority.High)).OrderByDescending(t => t.TaskPriority);

            return mapper.Map<List<ToDoTaskView>>(tasks);
        }

        public async Task<List<ToDoTaskView>> GetDailyTasks(Guid userId)
        {
            var tasks = await repository.GetAllAsync(t => t.CreatorId == userId && t.Folders.Any(f => f.TaskFolder.FolderType == FolderType.MyDay));

            return mapper.Map<List<ToDoTaskView>>(tasks);
        }

        public async Task<List<ToDoTaskShortView>> GetUserTasksByFolder(Guid folderId, Guid userId)
        {
            var tasks = await repository.GetAllAsync(t => t.Folders.Any(f => f.TaskFolderId == folderId) && t.CreatorId == userId);
            return mapper.Map<List<ToDoTaskShortView>>(tasks);
        }

        public async Task MoveTaskToFolder(Guid taskId, Guid folderId)
        {
            var task = await repository.GetAsync(taskId);
            task.Folders.FirstOrDefault(f => f.TaskFolder.FolderType == FolderType.Default).TaskFolderId = folderId;
            await repository.UpdateAsync(task);
        }
    }
}
