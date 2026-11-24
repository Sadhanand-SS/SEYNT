using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Features.Auth.Dtos;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Auth.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    IApplicationDbContext context,
    IJwtService jwtService)
    : IRequestHandler<RefreshTokenCommand, AuthResponseDto>
{
    private const int RefreshTokenExpiryDays = 7;

    public async Task<AuthResponseDto> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var incomingTokenHash = jwtService.HashRefreshToken(request.RefreshToken);

        var user = await context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(
                u => u.RefreshTokenHash == incomingTokenHash &&
                     u.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (user is null ||
            user.RefreshTokenExpiry is null ||
            user.RefreshTokenExpiry <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Invalid or expired refresh token.");
        }

        var accessToken = jwtService.GenerateAccessToken(user);
        var newRefreshToken = jwtService.GenerateRefreshToken();

        user.RefreshTokenHash = jwtService.HashRefreshToken(newRefreshToken);
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

        await context.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            UserRoleId = user.UserRoleId,
            RoleName = user.UserRole.Name,
            OrganisationId = user.OrganisationId
        };
    }
}
