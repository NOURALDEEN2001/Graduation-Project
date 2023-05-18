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

        public async Task<UserManagerResponse> AuthenticateUser(string recipientEmail)
        {
            Random random = new Random();
            int otpValue = random.Next(100000, 999999);
            string otp = otpValue.ToString("D6");

            await _sendEmails.SendOTPEmail(recipientEmail,otp);
            return new UserManagerResponse
            {
                IsSuccess = true,
                Message = otp
            };
        }

       
    }
}
