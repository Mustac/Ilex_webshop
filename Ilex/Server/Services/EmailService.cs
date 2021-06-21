using Ilex.Server.Services.Contracts;
using Ilex.Shared.Helpers;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ilex.Server.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(string emailAddress, string emailMessage, string emailSubject)
        {
            MailjetClient client = new MailjetClient( _config["MailJet:ApiKey"],_config["MailJet:SecretKey"]);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            };

            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                    .WithFrom(new SendContact("mustac.marijan@gmail.com"))
                    .WithSubject(emailSubject)
                    .WithHtmlPart(emailMessage)
                    .WithTo(new SendContact(emailAddress))
                    .Build();

            // invoke API to send email
            var responses = await client.SendTransactionalEmailAsync(email);
            Console.WriteLine(responses.Messages[0].Status);

            return responses.Messages.FirstOrDefault().Status == "success" ? true : false;
        }
       
    }
}
