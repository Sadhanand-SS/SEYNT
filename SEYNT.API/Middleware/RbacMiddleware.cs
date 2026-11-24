using System.Text.Json;
using SEYNT.Application.Common.Interfaces;
using SEYNT.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace SEYNT.API.Middleware;

public class RbacMiddleware
{
    private readonly RequestDelegate _next;

    public RbacMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentUserService currentUserService)
    {
        var endpoint = context.GetEndpoint();
        var actionDescriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

        if (actionDescriptor is null || context.User.Identity?.IsAuthenticated != true)
        {
            await _next(context);
            return;
        }

        // Endpoints every signed-in user is entitled to opt out of the rights lookup
        // with [AuthenticatedOnly]. Authentication has already happened at this point.
        if (endpoint!.Metadata.GetMetadata<AuthenticatedOnlyAttribute>() is not null)
        {
            await _next(context);
            return;
        }

        var requiredAction = ResolveRequiredAction(actionDescriptor.ActionName);
        if (requiredAction is null)
        {
            await _next(context);
            return;
        }

        // A controller is its module: RolesController is checked against the "Roles" rights entry.
        var moduleName = actionDescriptor.ControllerName;

        var rights = JsonSerializer.Deserialize<List<UserRightEntity>>(
            currentUserService.UserRights,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? [];

        var moduleRight = rights.FirstOrDefault(
            r => string.Equals(r.Controller, moduleName, StringComparison.OrdinalIgnoreCase));

        if (moduleRight is null || !HasPermission(moduleRight, requiredAction))
        {
            throw new UnauthorizedAccessException(
                $"Role '{currentUserService.Role}' does not have '{requiredAction}' permission on the '{moduleName}' module.");
        }

        await _next(context);
    }

    private static string? ResolveRequiredAction(string actionName)
    {
        if (actionName.StartsWith("Create", StringComparison.Ordinal)) return "Create";
        if (actionName.StartsWith("Update", StringComparison.Ordinal)) return "Edit";
        if (actionName.StartsWith("Change", StringComparison.Ordinal)) return "Edit";
        if (actionName.StartsWith("Delete", StringComparison.Ordinal)) return "Delete";
        if (actionName.StartsWith("Approve", StringComparison.Ordinal)) return "Approve";
        if (actionName.StartsWith("Get", StringComparison.Ordinal) ||
            actionName.StartsWith("Search", StringComparison.Ordinal) ||
            actionName.StartsWith("Verify", StringComparison.Ordinal)) return "View";

        return null;
    }

    private static bool HasPermission(UserRightEntity right, string requiredAction) => requiredAction switch
    {
        "Create" => right.Create,
        "Edit" => right.Edit,
        "Delete" => right.Delete,
        "Approve" => right.Approve,
        "View" => right.View,
        _ => false
    };
}
