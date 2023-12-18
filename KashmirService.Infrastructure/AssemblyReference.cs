using KashmirService.Infrastructure.ExceptionNotifier;
using KashmirService.Infrastructure.Identity;
using KashmirService.Infrastructure.TemplateRenderer;
using KashmirServices.Application.Abstractions.ExceptionNotifier;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IEmailService;
using KashmirServices.Application.Abstractions.JWT;
using KashmirServices.Application.Abstractions.TemplateRenderer;
using KashmirServices.Infrastructure.EmailService.MailJetServices;
using KashmirServices.Infrastructure.JWT;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KashmirServices.Infrastructure;

public static partial class AssemblyReference
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MailJetOptions>(configuration.GetSection("MailJetOptionSection"));
        services.AddTransient<IEmailService, MailJetEmailService>();
        services.AddScoped<IEmailTemplateRenderer, EmailTemplateRenderer>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddSingleton<IContextService, ContextService>();
        services.AddTransient<IExceptionNotifier, EmailExceptionLogger>();

        return services;
    }
}