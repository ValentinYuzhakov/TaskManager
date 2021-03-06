using FluentEmail.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Services.Interfaces;

namespace TaskManager.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail fluentEmail;

        public EmailService(IFluentEmail fluentEmail)
        {
            this.fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string message, string email)
        {
            await fluentEmail
                .To(email)
                .Body(message)
                .SendAsync();
        }

        public async Task SendConfirmationEmailAsync(string token, string email = "")
        {
            var message = $"http://localhost:5000/api/Identity/confirm-email/{token}";

            await fluentEmail
                .To(email)
                .Body(message)
                .SendAsync();
        }

    }
}
