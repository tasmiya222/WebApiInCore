using ApiForCrud.Helper;
using ApiForCrud.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ApiForCrud.Container
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
           this._emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_emailSettings.Email);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();


                var smtp = new SmtpClient();
                smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            
        }    
    }
}
