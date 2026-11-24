using Serilog.Context;

namespace SEYNT.API.Middleware;

/// <summary>
/// Assigns every request a correlation id (honouring an inbound X-Correlation-ID
/// header, else a new GUID), pushes it into the Serilog LogContext so every log
/// event in the request carries it, and echoes it back in the response header.
/// Must be the outermost middleware so the id covers the whole pipeline.
/// </summary>
public class CorrelationIdMiddleware
{
    public const string HeaderName = "X-Correlation-ID";

    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId =
            context.Request.Headers.TryGetValue(HeaderName, out var inbound) &&
            !string.IsNullOrWhiteSpace(inbound.FirstOrDefault())
                ? inbound.First()!
                : Guid.NewGuid().ToString();

        context.Response.Headers[HeaderName] = correlationId;

        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await _next(context);
        }
    }
}
