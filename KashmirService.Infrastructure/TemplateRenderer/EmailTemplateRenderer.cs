using KashmirServices.Application.Abstractions.TemplateRenderer;
using KashmirServices.Application.Shared;
using RazorLight;
using System.Reflection;

namespace KashmirService.Infrastructure.TemplateRenderer;

internal sealed class EmailTemplateRenderer : IEmailTemplateRenderer
{
    public async Task<string> RenderTemplateAsync(string templateName, object model)
    {
        string templateResult = string.Empty;

        try
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string assemblyDirectory = Path.GetDirectoryName(assemblyLocation);

            var templateFolder = Path.Combine(assemblyDirectory, "EmailTemplates");

            var engine = new RazorLightEngineBuilder()
            .UseFileSystemProject(templateFolder)
            .UseMemoryCachingProvider()
            .EnableDebugMode()
            .Build();
            templateResult = await engine.CompileRenderAsync(templateName, model);
        }
        catch (Exception)
        {
            throw;
        }

        return templateResult;
    }
}