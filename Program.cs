using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EP210_MimeKitSendEmailToSmtp4Dev
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("System", "system@test.com"));
            emailMessage.To.Add(new MailboxAddress("Dave Paquette", "dave@dave.com"));
            emailMessage.Subject = "This is a test";
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = "<h1>Important message</h1><p>This is from the system</p>";
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                var secureSocketOptions =  SecureSocketOptions.None;
                client.Connect("localhost", 25, secureSocketOptions);
                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }            
        }
    }
}
