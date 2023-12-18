namespace KashmirServices.Application.Abstractions.TemplateRenderer;

public interface IEmailTemplateRenderer
{
    Task<string> RenderTemplateAsync(string templateName, object model);
}
