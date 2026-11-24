using System.Text.Json;
using SEYNT.Application.Exceptions;
using FluentValidation;

namespace SEYNT.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int statusCode;
        string message;
        var errors = new List<string>();

        switch (exception)
        {
            case ValidationException validationException:
                statusCode = StatusCodes.Status400BadRequest;
                message = "One or more validation errors occurred.";
                errors.AddRange(validationException.Errors.Select(e =>
                    string.IsNullOrEmpty(e.PropertyName)
                        ? e.ErrorMessage
                        : $"{e.PropertyName}: {e.ErrorMessage}"));
                break;

            case KeyNotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = exception.Message;
                break;

            case UnauthorizedAccessException:
                statusCode = StatusCodes.Status401Unauthorized;
                message = exception.Message;
                break;

            case NotFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = exception.Message;
                break;

            case ConflictException:
                statusCode = StatusCodes.Status409Conflict;
                message = exception.Message;
                break;

            case TooManyRequestsException:
                statusCode = StatusCodes.Status429TooManyRequests;
                message = exception.Message;
                break;

            case BusinessRuleException:
                statusCode = StatusCodes.Status422UnprocessableEntity;
                message = exception.Message;
                break;

            case ArgumentOutOfRangeException:
                statusCode = StatusCodes.Status400BadRequest;
                message = exception.Message;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        if (statusCode == StatusCodes.Status500InternalServerError)
            _logger.LogError(exception, "Unhandled exception");
        else
            _logger.LogWarning(
                exception,
                "Request failed with {ExceptionType}: {ErrorMessage}",
                exception.GetType().Name,
                exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var traceId = context.Response.Headers[CorrelationIdMiddleware.HeaderName].ToString();

        var response = new
        {
            status = statusCode,
            message,
            errors,
            traceId
        };

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}
