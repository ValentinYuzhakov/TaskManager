using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Data.Services.Interfaces;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/ToDoTask")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ITodoTaskService taskService;


        public ToDoTaskController(ITodoTaskService taskService)
        {
            this.taskService = taskService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateToDoTask([FromBody] CreateTodoTaskInfo request)
        {
            await taskService.CreateToDoTask(request);
            return Ok();
        }

        [HttpGet("{taskId}")]
        public async Task<ToDoTaskView> GetTask(Guid taskId)
        {
            return await taskService.GetById(taskId);
        }

        [HttpGet("all/{userId}")]
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
    }
}
