using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYP_Project_API.Application.Interfaces.Services
{
    public interface IEmailService
    {
        void SendEmail(string emailBody, string subject, List<string> receivers);
        bool CheckEmail(string receiver);
    }
}
