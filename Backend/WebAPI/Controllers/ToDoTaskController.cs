using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Data.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos;

namespace WebAPI.Controllers
{
    [Route("api/ToDoTask")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly ITodoTaskService taskService;
        private DatabaseContext context;


        public ToDoTaskController(ITodoTaskService taskService, DatabaseContext context)
        {
            this.taskService = taskService;
            this.context = context;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> CreateToDoTask([FromBody] CreateTodoTaskInfo request)
        {
            await taskService.CreateToDoTask(request);
            return Ok();
        }

        [HttpGet("get")]
        public async Task<List<ToDoTask>> Get()
        {
            var tasks = context.Tasks.ToList();
            return tasks;
        }

    }
}
