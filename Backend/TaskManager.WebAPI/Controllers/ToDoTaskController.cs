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
    [Route("api/ToDoTask")]
    [ApiController]
    public class ToDoTaskController : SystemController
    {
        private readonly ITodoTaskService taskService;


        public ToDoTaskController(ITodoTaskService taskService,
            UserManager<User> userManager) : base(userManager)
        {
            this.taskService = taskService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateToDoTask([FromBody] CreateTodoTaskInfo request)
        {
            await taskService.CreateToDoTask(request);
            return Ok();
        }

        [HttpDelete("{taskId:guid}")]
        public async Task<IActionResult> DeleteToDoTask(Guid taskId)
        {
            await taskService.DeleteToDoTask(taskId);
            return Ok();
        }

        [HttpGet("{taskId:guid}")]
        public async Task<ToDoTaskView> GetTask(Guid taskId)
        {
            return await taskService.GetById(taskId);
        }

        [HttpGet("all/{userId:guid}")]
        public async Task<List<ToDoTaskView>> GetTasksByUser(Guid userId)
        {
            return await taskService.GetTasksByUser(userId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateToDoTaskInfo request)
        {
            await taskService.UpdateToDoTask(request);
            return Ok();
        }

        [HttpPut("update/priority")]
        public async Task<IActionResult> UpdateTaskPriority([FromBody] UpdateToDoTaskPriorityInfo request)
        {
            await taskService.UpdatePriority(request);
            return Ok();
        }

        [HttpPut("update/status")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] UpdateToDoTaskStatusInfo request)
        {
            await taskService.UpdateStatus(request);
            return Ok();
        }

        [HttpGet("done/{userId:guid}")]
        public async Task<List<ToDoTaskView>> GetDoneTasks(Guid userId)
        {
            return await taskService.GetDoneTasks(userId);
        }

        [HttpGet("important/{userId:guid}")]
        public async Task<List<ToDoTaskView>> GetImportantTasks(Guid userId)
        {
            return await taskService.GetImportantTasks(userId);
        }

        [HttpGet("daily")]
        public async Task<List<ToDoTaskView>> GetDailyTasks(Guid userId)
        {
            return await taskService.GetDailyTasks(userId);
        }

        [HttpPut("move/{folderId:guid}/{taskId:guid}")]
        public async Task<IActionResult> MoveTaskToFolder(Guid folderId, Guid taskId)
        {
            await taskService.MoveTaskToFolder(taskId, folderId);
            return Ok();
        }

        [HttpGet("{folderId:guid}/{userId:guid}")]
        public async Task<List<ToDoTaskShortView>> GetTasksByFolder(Guid folderId, Guid userId)
        {
            return await taskService.GetUserTasksByFolder(folderId, userId);
        }
    }
}
