using System.Security.Claims;
using Serilog;
using Serilog.Context;

namespace SEYNT.API.Middleware;

/// <summary>
/// Pushes the authenticated user's id into the Serilog LogContext (every log
/// event downstream) and the diagnostic context (the request-completion event).
/// Runs after UseAuthentication; anonymous requests simply carry no UserId.
/// </summary>
public class UserEnrichmentMiddleware
{
    private readonly RequestDelegate _next;

    public UserEnrichmentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IDiagnosticContext diagnosticContext)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            await _next(context);
            return;
        }

        diagnosticContext.Set("UserId", userId);

        using (LogContext.PushProperty("UserId", userId))
        {
            await _next(context);
        }
    }
}
