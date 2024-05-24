using ApiForCrud.Helper;

namespace ApiForCrud.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
