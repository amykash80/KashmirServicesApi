using KashmirServices.Application.Abstractions.IEmailService;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Options;

namespace KashmirServices.Infrastructure.EmailService.MailJetServices;

public class MailJetEmailService : IEmailService
{
    private readonly MailJetOptions mailJetOptions;

    public MailJetEmailService(IOptions<MailJetOptions> mailJetOptions)
    {
        this.mailJetOptions = mailJetOptions.Value;
    }
    public async Task<bool> SendEmailAsync(MailSetting mailSetting)
    {
        MailjetClient mailjetClient = new(mailJetOptions?.ApiKey, mailJetOptions?.SecretKey);

        var email = new TransactionalEmailBuilder()
               .WithFrom(new SendContact(mailJetOptions?.FromEmail))
               .WithSubject(mailSetting.Subject)
               .WithHtmlPart(mailSetting.Body)
               .WithTo(new SendContact(mailSetting.To.FirstOrDefault()))
               .Build();

        var emailResult = await mailjetClient.SendTransactionalEmailAsync(email);

        return true;
    }
}
