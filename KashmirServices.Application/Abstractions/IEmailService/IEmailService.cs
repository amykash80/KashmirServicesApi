namespace KashmirServices.Application.Abstractions.IEmailService;

public interface IEmailService
{
    Task<bool> SendEmailAsync(MailSetting settings);
}
