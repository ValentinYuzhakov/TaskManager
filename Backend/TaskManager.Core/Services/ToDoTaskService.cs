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
        private readonly IToDoTaskRepository toDoTaskrepository;
        private readonly ITaskFolderService taskFolderService;


        public ToDoTaskService(IMapper mapper,
            IToDoTaskRepository toDoTaskrepository,
            ITaskFolderService taskFolderService)
        {
            this.mapper = mapper;
            this.toDoTaskrepository = toDoTaskrepository;
            this.taskFolderService = taskFolderService;
        }


        public async Task Create(CreateTodoTaskInfo taskInfo)
        {
            var task = mapper.Map<ToDoTask>(taskInfo);

            if (taskInfo.FolderId.HasValue)
            {
                var folder = await taskFolderService.GetById(taskInfo.FolderId.Value);

                task.Folders.Add(new TaskFolderTodoTask
                {
                    TaskFolderId = taskInfo.FolderId.Value,
                    TaskFolder = folder
                });
            }
            else
            {
                var folder = await taskFolderService.GetSystemFolder(FolderType.Tasks);
                task.Folders.Add(new TaskFolderTodoTask
                {
                    TaskFolderId = folder.Id,
                    TaskFolder = folder
                });
            }

            await toDoTaskrepository.CreateAsync(task);
        }

        public async Task Update(UpdateToDoTaskInfo taskinfo)
        {
            var taskToUpdate = await toDoTaskrepository.GetAsync(taskinfo.Id);
            var todoTask = mapper.Map(taskinfo, taskToUpdate);
            await toDoTaskrepository.UpdateAsync(todoTask);
        }

        public async Task Delete(Guid taskId)
        {
            await toDoTaskrepository.DeleteAsync(taskId);
        }

        public async Task<ToDoTaskView> GetById(Guid taskId)
        {
            var task = await toDoTaskrepository.GetAsync(taskId);
            var taskView = mapper.Map<ToDoTaskView>(task);
            return taskView;
        }

        public async Task<List<ToDoTaskShortView>> GetAllByUser(Guid userId)
        {
            var tasks = await toDoTaskrepository.GetAllAsync(t => t.CreatorId == userId);
            return mapper.Map<List<ToDoTaskShortView>>(tasks);
        }

        public async Task UpdatePriority(UpdateToDoTaskPriorityInfo taskInfo)
        {
            var task = await toDoTaskrepository.GetAsync(taskInfo.Id);
            task.TaskPriority = Enum.Parse<TaskPriority>(taskInfo.TaskPriority);

            await toDoTaskrepository.UpdateAsync(task);
        }

        public async Task UpdateStatus(UpdateToDoTaskStatusInfo taskInfo)
        {
            var task = await toDoTaskrepository.GetAsync(taskInfo.Id);
            task.TaskStatus = Enum.Parse<TaskManager.Domain.Enums.TaskStatus>(taskInfo.TaskStatus);

            await toDoTaskrepository.UpdateAsync(task);
        }

        public async Task<List<ToDoTaskShortView>> GetDoneTasks(Guid userId)
        {
            var tasks = await toDoTaskrepository.GetAllAsync(t => t.CreatorId == userId &&
                t.TaskStatus == Domain.Enums.TaskStatus.Done);

            return mapper.Map<List<ToDoTaskShortView>>(tasks);
        }

        public async Task<List<ToDoTaskShortView>> GetImportantTasks(Guid userId)
        {
            var tasks = (await toDoTaskrepository.GetAllAsync(t => t.CreatorId == userId &&
                t.TaskPriority == TaskPriority.Highest || t.TaskPriority == TaskPriority.High)).OrderByDescending(t => t.TaskPriority);

            return mapper.Map<List<ToDoTaskShortView>>(tasks);
        }

        public async Task<List<ToDoTaskShortView>> GetDailyTasks(Guid userId)
        {
            var tasks = await toDoTaskrepository.GetAllAsync(t => t.CreatorId == userId && t.Folders.Any(f => f.TaskFolder.FolderType == FolderType.MyDay));

            return mapper.Map<List<ToDoTaskShortView>>(tasks);
        }

        public async Task<List<ToDoTaskShortView>> GetPlannedTasks(Guid userId)
        {
            var plannedTasks = await toDoTaskrepository.GetAllAsync(t => t.CreatorId == userId && t.EndDate != null);
            return mapper.Map<List<ToDoTaskShortView>>(plannedTasks);
        }

        public async Task<List<ToDoTaskShortView>> GetUserTasksByFolder(Guid folderId, Guid userId)
        {
            var tasks = await toDoTaskrepository.GetAllAsync(t => t.Folders.Any(f => f.TaskFolderId == folderId) && t.CreatorId == userId);
            return mapper.Map<List<ToDoTaskShortView>>(tasks);
        }

        public async Task MoveToFolder(Guid taskId, Guid folderId)
        {
            var task = await toDoTaskrepository.GetAsync(taskId);
            task.Folders.FirstOrDefault(f => f.TaskFolder.FolderType == FolderType.Default).TaskFolderId = folderId;
            await toDoTaskrepository.UpdateAsync(task);
        }
    }
}
