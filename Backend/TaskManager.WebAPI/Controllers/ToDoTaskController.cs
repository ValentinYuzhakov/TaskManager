using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.ToDoTasks;
using TaskManager.Shared.ShortViewModels;
using TaskManager.Shared.ViewModels;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/ToDoTask")]
    public class ToDoTaskController : SystemController
    {
        private readonly ITodoTaskService toDoTaskService;


        public ToDoTaskController(ITodoTaskService taskService,
            UserManager<User> userManager) : base(userManager)
        {
            this.toDoTaskService = taskService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateToDoTask([FromBody] CreateTodoTaskInfo request)
        {
            var taskId = await toDoTaskService.Create(request);
            return Ok(taskId);
        }

        [HttpDelete("{taskId:guid}")]
        public async Task<IActionResult> DeleteToDoTask(Guid taskId)
        {
            await toDoTaskService.Delete(taskId);
            return Ok();
        }

        [HttpGet("{taskId:guid}")]
        public async Task<ToDoTaskView> GetToDoTask(Guid taskId)
        {
            return await toDoTaskService.FindById(taskId);
        }

        [HttpGet("all/{userId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetTasksByUser(Guid userId)
        {
            return await toDoTaskService.GetAllByUser(userId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateToDoTask([FromBody] UpdateToDoTaskInfo request)
        {
            await toDoTaskService.Update(request);
            return Ok();
        }

        [HttpPut("update-priority")]
        public async Task<IActionResult> UpdateTaskPriority([FromBody] UpdateToDoTaskPriorityInfo request)
        {
            await toDoTaskService.UpdatePriority(request);
            return Ok();
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateToDoTaskStatusInfo request)
        {
            await toDoTaskService.UpdateStatus(request);
            return Ok();
        }

        [HttpGet("done/{userId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetDoneTasks(Guid userId)
        {
            return await toDoTaskService.GetDoneTasks(userId);
        }

        [HttpGet("important/{userId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetImportantTasks(Guid userId)
        {
            return await toDoTaskService.GetImportantTasks(userId);
        }

        [HttpGet("daily/{userId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetDailyTasks(Guid userId)
        {
            return await toDoTaskService.GetDailyTasks(userId);
        }

        [HttpGet("planned/{userId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetPlannedTasks(Guid userId)
        {
            return await toDoTaskService.GetPlannedTasks(userId);
        }

        [HttpPut("move/{folderId:guid}/{taskId:guid}")]
        public async Task<IActionResult> MoveTaskToFolder(Guid folderId, Guid taskId)
        {
            await toDoTaskService.MoveToFolder(taskId, folderId);
            return Ok();
        }

        [HttpGet("{folderId:guid}/{userId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetTasksByFolder(Guid folderId, Guid userId)
        {
            return await toDoTaskService.GetUserTasksByFolder(folderId, userId);
        }
    }
}
