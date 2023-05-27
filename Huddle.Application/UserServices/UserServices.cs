using Huddle.Application.EmailService;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huddle.Application.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly ISendEmails _sendEmails;
        public UserServices(ISendEmails sendEmails)
        {
            _sendEmails = sendEmails;
        }

        public async Task<UserManagerResponse<string>> AuthenticateUser(string recipientEmail)
        {
            Random random = new Random();
            int otpValue = random.Next(100000, 999999);
            string otp = otpValue.ToString("D6");

            string subject = "Huddle Account Authentication";
            string body = $"Here is your OTP verification code : {otp}, Please DO NOT SHARE IT.";
            var message = new Message(new string[] { recipientEmail }, subject, body);
            await _sendEmails.SendEmail(message);
            return new UserManagerResponse<string>
            {
                IsSuccess = true,
                Message = otp
            };
        }

       
    }
}
