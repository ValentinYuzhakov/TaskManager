﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string message, string email);
        Task SendConfirmationEmailAsync(string token, string email = "yuz-valya@yandex.ru");
    }
}
