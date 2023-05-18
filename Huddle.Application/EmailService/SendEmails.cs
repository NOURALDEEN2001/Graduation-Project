using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Huddle.Application.EmailService
{
    public class SendEmails:ISendEmails
    {
        public async Task SendOTPEmail(string recipientEmail,string OTP)
        {
            try
            {
                var fromAddress = new MailAddress("huddlepsut@gmail.com", "Server");
                var toAddress = new MailAddress(recipientEmail, "Recipient");
                const string fromPassword = "Huddle.psut.123";
                const string subject = "Huddle Account Authentication";
                string body = $"Here is your OTP verification code : {OTP}, Please DO NOT SHARE IT.";
                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);

                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        await smtp.SendMailAsync(message);
                    }
                }
               
            }

            catch (SmtpException ex)
            {
                Console.WriteLine("SMTP Exception: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        
    }
      
}
