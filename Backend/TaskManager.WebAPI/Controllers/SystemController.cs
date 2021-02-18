using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskManager.Domain.Models;

namespace TaskManager.WebAPI.Controllers
{
    public class SystemController : ControllerBase
    {
        protected Guid CurrentUserId { get; }

        public SystemController(UserManager<User> userManager)
        {
            CurrentUserId = userManager.GetUserAsync(HttpContext.User).Result.Id;
        }
    }
}
