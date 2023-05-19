using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace Huddle.Application.EmailService
{
    public class SendEmails : ISendEmails
    {

        private readonly EmailConfiguration _emailConfiguration;
        public SendEmails(EmailConfiguration emailConfiguration)
        {   
            _emailConfiguration = emailConfiguration;
        }
        public async Task SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            await Send(emailMessage);















            //try
            //{
            //var fromAddress = new MailAddress("huddlepsut@gmail.com", "Server");
            //var toAddress = new MailAddress(recipientEmail, "Recipient");
            //const string fromPassword = "Huddle.psut.123";
            //const string subject = "Huddle Account Authentication";
            //string body = $"Here is your OTP verification code : {OTP}, Please DO NOT SHARE IT.";
            //using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    smtp.EnableSsl = true;
            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);

            //    using (var message = new MailMessage(fromAddress, toAddress)
            //    {
            //        Subject = subject,
            //        Body = body
            //    })
            //    {
            //        await smtp.SendMailAsync(message);
            //    }
            //}

            //var smtpClient = new SmtpClient("smtp.gmail.com")
            //{
            //    Port = 587,
            //    Credentials = new NetworkCredential("huddlepsut@gmail.com", "Huddle.psut.123"),
            //    EnableSsl = true,
            //};

            //smtpClient.Send("huddlepsut@gmail.com", recipientEmail, subject, body);
            // }

            //catch (SmtpException ex)
            //{
            //    Console.WriteLine("SMTP Exception: " + ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception: " + ex.Message);
            //}

            //string to = recipientEmail; //To address    
            //string from = "huddlepsut@gmail.com"; //From address    
            //MailMessage message = new MailMessage(from, to);

            //string mailbody = $"Here is your OTP verification code : {OTP}, Please DO NOT SHARE IT.";
            //message.Subject = "Huddle Account Authentication";
            //message.Body = mailbody;
            ////message.BodyEncoding = Encoding.UTF8;
            ////message.IsBodyHtml = true;
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 465); //Godaddy smtp    
            //System.Net.NetworkCredential basicCredential1 = new
            //System.Net.NetworkCredential("info@huddlepsut@gmail.com", "Huddle.psut.123");
            //client.EnableSsl = true;
            ////client.UseDefaultCredentials = false;
            //client.Credentials = basicCredential1;

            //try
            //{
            //    client.Send(message);
            //}

            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email",_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        }

        private async Task Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer,_emailConfiguration.Port,true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
