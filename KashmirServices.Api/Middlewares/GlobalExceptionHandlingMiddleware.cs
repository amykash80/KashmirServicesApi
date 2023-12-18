using KashmirServices.Api.Utils;
using KashmirServices.Application.Abstractions.ExceptionNotifier;
using KashmirServices.Application.Shared;
using KashmirServices.Application.Utils.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace KashmirServices.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> logger;
    private readonly IExceptionNotifier exceptionNotifier;
    private readonly IWebHostEnvironment webHostEnvironment;

    public GlobalExceptionHandlingMiddleware(
        ILogger<GlobalExceptionHandlingMiddleware> logger,
        IExceptionNotifier exceptionNotifier,
        IWebHostEnvironment webHostEnvironment)
    {
        this.logger = logger;
        this.exceptionNotifier = exceptionNotifier;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        List<APIError> errors = new();
        ProblemDetails problemDetails;

        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            logger.LogError(e, message: e.Message);

            switch (e)
            {
                case APIValidationException ex:
                    context.Response.StatusCode = APIStatusCodes.BadRequest;
                    errors = ex.Errors;

                    problemDetails = new ExtendedProblemDetails
                    {
                        Type = GetExceptionType(ex),
                        Title = APIMessages.ValidationException,
                        Status = APIStatusCodes.BadRequest,
                        Detail = GetErrorMessages(ex.Errors),
                        Instance = ex!.InnerException is null ? "" : ex.InnerException.Message,
                        Errors = errors 
                    };

                break;

                case ForbiddenException ex:
                    context.Response.StatusCode = APIStatusCodes.Forbidden;
                    errors.Add(new APIError(ex.Message));

                    problemDetails = new ExtendedProblemDetails
                    {
                        Type = GetExceptionType(ex),
                        Title = APIMessages.ForbiddenException,
                        Status = APIStatusCodes.Forbidden,
                        Detail = ex.Message,
                        Instance = ex!.InnerException is null ? "" : ex.InnerException.Message,
                        Errors = errors
                    };
                    break;

                case APIInfoException ex:
                    context.Response.StatusCode = APIStatusCodes.Conflict;
                    errors.Add(new APIError(ex.Message));

                    problemDetails = new ExtendedProblemDetails
                    {
                        Type = GetExceptionType(ex),
                        Title = APIMessages.InfoOrAndConflictException,
                        Status = APIStatusCodes.Conflict,
                        Detail = ex.Message,
                        Instance = ex!.InnerException is null ? "" : ex.InnerException.Message,
                        Errors = errors
                    };
                    break;

                case DbUpdateException ex:
                    context.Response.StatusCode = APIStatusCodes.BadRequest;
                    errors.Add(new APIError(ex.Message));

                    problemDetails = new ExtendedProblemDetails
                    {
                        Type = GetExceptionType(ex),
                        Title = APIMessages.DbUpdateException,
                        Status = APIStatusCodes.BadRequest,
                        Detail = ex.Message,
                        Instance = ex!.InnerException is null ? "" : ex.InnerException.Message,
                        Errors = errors
                    };
                    break;


                case SqlException ex:
                    context.Response.StatusCode = APIStatusCodes.BadRequest;
                    errors.Add(new APIError(ex.Message));

                    problemDetails = new ExtendedProblemDetails
                    {
                        Type = GetExceptionType(ex),
                        Title = APIMessages.DbUpdateException,
                        Status = APIStatusCodes.BadRequest,
                        Detail = ex.Message,
                        Instance = ex!.InnerException is null ? "" : ex.InnerException.Message,
                        Errors = errors
                    };
                    break;

                default:
                    var message = webHostEnvironment.IsDevelopment() ? e.Message : "One or more errors occurred";
                    //exceptionNotifier.LogToEmail(e);

                    context.Response.StatusCode =
                              APIStatusCodes.InternalServerError;
                    errors.Add(new APIError(message));

                    problemDetails = new ExtendedProblemDetails
                    {
                        Type = nameof(APIStatusCodes.InternalServerError),
                        Title = message,
                        Status = APIStatusCodes.InternalServerError,
                        Detail = message,
                        Errors = new List<APIError> { },
                    };

                break;
            }

            string json = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }

    #region Private Methods
    private string GetExceptionType(Exception ex)
    {
        string methodName = $"{ex.TargetSite?.DeclaringType?.FullName}.{ex.TargetSite?.Name}";
        string assemblyName = Assembly.GetEntryAssembly()?.GetName().Name! ?? "ApplicationAssembly";

        return $"Layer / Assembly Address And Method / Function Name: {assemblyName} {methodName}";
    }

    private string GetErrorMessages(List<APIError> errors)
    {
        string message = string.Empty;
        foreach (var error in errors)
        {
            message += $"{error.Message} {error.Field}";
        }

        return $"{message}";
    }
    #endregion
}
