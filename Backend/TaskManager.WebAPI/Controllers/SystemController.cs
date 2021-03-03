using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskManager.Domain.Models;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace TaskManager.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly UserManager<User> userManager;


        public SystemController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }


        protected Guid CurrentUserId => Guid.Parse(userManager.GetUserId(User));
    }
}
