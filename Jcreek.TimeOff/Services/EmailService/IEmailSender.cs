using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jcreek.TimeOff.Services.EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);

        Task SendEmailAsync(Message message);
    }
}
