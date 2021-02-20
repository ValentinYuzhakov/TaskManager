﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;
using TaskManager.Shared.Infos.TaskFolders;
using TaskManager.Shared.ShortViewModels;

namespace TaskManager.WebAPI.Controllers
{
    [Route("api/TaskFolder")]
    [ApiController]
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
            await taskFolderService.CreateFolder(request);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateFolder([FromBody] UpdateTaskFolderInfo request)
        {
            await taskFolderService.UpdateFolder(request);
            return Ok();
        }

        [HttpDelete("delete/{folderId:guid}")]
        public async Task<IActionResult> DeleteFolder(Guid folderId)
        {
            await taskFolderService.DeleteFolder(folderId);
            return Ok();
        }

        [HttpGet("{userId:guid}")]
        public async Task<List<TaskFolderShortView>> GetFoldersByUser(Guid userId)
        {
            return await taskFolderService.GetFoldersByUser(userId);
        }
    }
}
