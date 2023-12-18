namespace KashmirServices.Infrastructure.EmailService.MailJetServices;

public class MailJetOptions
{
    public string ApiKey { get; set; } = null!;


    public string SecretKey { get; set; } = null!;


    public string FromEmail { get; set; } = null!;


    public string FromName { get; set; } = null!;
}
