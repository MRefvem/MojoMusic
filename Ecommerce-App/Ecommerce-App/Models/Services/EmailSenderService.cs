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
        private IConfiguration _configuration;

        // brings in the configuration
        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// sends a email
        /// </summary>
        /// <param name="email">the email to send to</param>
        /// <param name="subject">the subject of the email</param>
        /// <param name="htmlMessage">message </param>
        /// <returns>task completion</returns>
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
