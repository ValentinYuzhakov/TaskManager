using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Shared.Infos.SubTasks;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/SubTask")]
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskService subTaskService;


        public SubTaskController(ISubTaskService subTaskService)
        {
            this.subTaskService = subTaskService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateSubTask([FromBody] SubTaskCreateInfo request)
        {
            var subTaskId = await subTaskService.Create(request);
            return Ok(subTaskId);
        }

        [HttpDelete("delete/{subTaskId:guid}")]
        public async Task<IActionResult> DeleteSubTask(Guid subTaskId)
        {
            await subTaskService.Delete(subTaskId);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSubTask([FromBody] UpdateSubTaskInfo request)
        {
            await subTaskService.Update(request);
            return Ok();
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateSubTaskStatusInfo request)
        {
            await subTaskService.UpdateStatus(request);
            return Ok();
        }
    }
}
