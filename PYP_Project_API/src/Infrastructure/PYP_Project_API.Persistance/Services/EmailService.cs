using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PYP_Project_API.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PYP_Project_API.Persistance.Services
{
    public class EmailService : IEmailService
    {
        public bool CheckEmail(string receiver)
        {
            Regex emailRegex =new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (emailRegex.IsMatch(receiver)&&receiver.Contains("code.edu.az"))return true;
            return false; 
        }

        public void SendEmail(string emailBody,string subject, List<string> receivers)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("test123pyp@gmail.com"));
            email.Subject =subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailBody };
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("test123pyp@gmail.com", "1234test");
            foreach (string receiver in receivers)
            {
                email.To.Add(MailboxAddress.Parse($"{receiver}"));
                smtp.Send(email);
            }
            smtp.Disconnect(true);
        }
    }
}
