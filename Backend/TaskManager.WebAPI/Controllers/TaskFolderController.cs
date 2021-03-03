using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.ViewModels;

namespace TaskManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/TaskFolder")]
    public class TaskFolderController : SystemController
    {
        private readonly ITaskFolderService taskFolderService;


        public TaskFolderController(ITaskFolderService taskFolderService,
            UserManager<User> userManager) : base(userManager)
        {
            this.taskFolderService = taskFolderService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateFolder([FromBody] CreateTaskFolderInfo request)
        {
            var folderId = await taskFolderService.Create(request);
            return Ok(folderId);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFolder([FromBody] UpdateTaskFolderInfo request)
        {
            await taskFolderService.Update(request);
            return Ok();
        }

        [HttpDelete("delete/{folderId:guid}")]
        public async Task<IActionResult> DeleteFolder(Guid folderId)
        {
            await taskFolderService.Delete(folderId);
            return Ok();
        }

        [HttpGet]
        public async Task<IReadOnlyList<TaskFolderView>> GetFoldersByUser()
        {
            return await taskFolderService.GetByUser(CurrentUserId);
        }
    }
}
