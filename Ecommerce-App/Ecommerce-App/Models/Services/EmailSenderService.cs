using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// SendEmailAsync - Method allows the program to send emails automatically.
        /// </summary>
        /// <param name="email">The address of the email account to send the message to.</param>
        /// <param name="subject">The subject of the message (will display in the subject field upon receipt).</param>
        /// <param name="htmlMessage">The HTML content of the message.</param>
        /// <returns>The task complete, an automated message was sent upon trigger of a specific action.</returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SendGridClient client = new SendGridClient(_configuration["SENDGRID_API_KEY"]);
            SendGridMessage message = new SendGridMessage();

            message.SetFrom("mojomusicCORP@gmail.com", "Mojo Music");
            message.AddTo(email);
            message.SetSubject(subject);
            message.AddContent(MimeType.Html, htmlMessage);

            await client.SendEmailAsync(message);
        }
    }
}
