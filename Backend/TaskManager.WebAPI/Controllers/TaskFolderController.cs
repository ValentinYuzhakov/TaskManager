using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers
{
    [Route("api/TaskFolder")]
    [ApiController]
    public class TaskFolderController : SystemController
    {
        private readonly ITaskFolderService taskFolderService;


        public TaskFolderController(ITaskFolderService taskFolderService, UserManager<User> userManager) : base(userManager)
        {
            this.taskFolderService = taskFolderService;
        }
    }
}
