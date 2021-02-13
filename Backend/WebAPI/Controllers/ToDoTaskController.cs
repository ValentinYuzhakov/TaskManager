using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Data.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;
using TaskManager.Shared.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/ToDoTask")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ITodoTaskService taskService;
        private readonly DatabaseContext context;


        public ToDoTaskController(ITodoTaskService taskService, DatabaseContext context)
        {
            this.taskService = taskService;
            this.context = context;
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
            var taskView = await taskService.GetById(taskId);
            return taskView;
        }

        [HttpGet("all/{userId}")]
        public async Task<List<ToDoTaskView>> GetTasksByUser(Guid userId)
        {
            return await taskService.GetTasksByUser(userId);
        }
    }
}
