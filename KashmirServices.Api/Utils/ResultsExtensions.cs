using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Shared;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.AspNetCore.Mvc;

public static class ResultsExtensions
{
    public static IResult APIResult<T>(this ApiController controller, APIResponse<T> result)
    {
        return new CustomResult<T>(result);
    }
}

public class CustomResult<T> : IResult
{
    public int StatusCode { get; }
    public APIResponse<T> Value { get; }

    public CustomResult(APIResponse<T> value)
    {
        Value = value;
        StatusCode = (int)value.StatusCode;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = StatusCode;
        httpContext.Response.ContentType = "application/json";

        var serializerSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() } 
        };

        var responseJson = JsonSerializer.Serialize(Value, serializerSettings);
        return httpContext.Response.WriteAsync(responseJson);
    }
}
