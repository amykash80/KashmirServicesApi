using KashmirServices.Application.Abstractions.ExceptionNotifier;
using KashmirServices.Application.Abstractions.IEmailService;
using Newtonsoft.Json;

namespace KashmirService.Infrastructure.ExceptionNotifier;

internal sealed class EmailExceptionLogger : IExceptionNotifier
{
    private readonly IEmailService emailService;

    public EmailExceptionLogger(
        IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public void LogToEmail(Exception ex)
    {

       // if ()

        var mailSetting = new MailSetting
        {
            //To = ,
            //CC = ,
            Body = JsonConvert.SerializeObject(ex, Formatting.Indented),
            Subject = ex.Message
        };

        emailService.SendEmailAsync(mailSetting);
    }
}


public sealed class EmailExceptionLoggerOptions
{
    public List<string> To { get; set; } = new List<string>();


    public List<string> CC { get; set; } = new List<string>();
}
