using SEYNT.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SEYNT.Infrastructure.Authentication;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var userId = httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return Guid.Parse(userId);
        }
    }

    public Guid OrganisationId
    {
        get
        {
            var organisationId = httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst("organisationId")?
                .Value;

            if (string.IsNullOrWhiteSpace(organisationId))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return Guid.Parse(organisationId);
        }
    }

    public string Role =>
        httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.Role)?
            .Value ?? string.Empty;

    public string UserRights =>
        httpContextAccessor.HttpContext?
            .User?
            .FindFirst("userRights")?
            .Value ?? "[]";
}
