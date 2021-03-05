using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("all")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetTasksByUser()
        {
            var context = await HttpContext.GetTokenAsync("Bearer", "access_token");
            return await toDoTaskService.GetAllByUser(CurrentUserId);
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

        [HttpGet("done")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetDoneTasks()
        {
            return await toDoTaskService.GetDoneTasks(CurrentUserId);
        }

        [HttpGet("important")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetImportantTasks()
        {
            return await toDoTaskService.GetImportantTasks(CurrentUserId);
        }

        [HttpGet("daily")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetDailyTasks()
        {
            return await toDoTaskService.GetDailyTasks(CurrentUserId);
        }

        [HttpGet("planned")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetPlannedTasks()
        {
            return await toDoTaskService.GetPlannedTasks(CurrentUserId);
        }

        [HttpPut("move/{folderId:guid}/{taskId:guid}")]
        public async Task<IActionResult> MoveTaskToFolder(Guid folderId, Guid taskId)
        {
            await toDoTaskService.MoveToFolder(taskId, folderId);
            return Ok();
        }

        [HttpGet("{folderId:guid}")]
        public async Task<IReadOnlyList<ToDoTaskShortView>> GetTasksByFolder(Guid folderId)
        {
            return await toDoTaskService.GetUserTasksByFolder(folderId, CurrentUserId);
        }
    }
}
