using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Application.EmailService
{
    public interface ISendEmails
    {
        public Task SendOTPEmail(string recipientEmail,string OTP);
    }
}
