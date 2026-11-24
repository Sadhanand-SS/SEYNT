using SEYNT.Application.Common.Extensions;
using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Features.Auth.Dtos;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler(
    IApplicationDbContext context,
    IJwtService jwtService)
    : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private const int RefreshTokenExpiryDays = 7;

    public async Task<AuthResponseDto> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var email = request.Email.NormalizeEmail();

        var user = await context.Users
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(
                u => u.Email.ToLower() == email &&
                     u.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (user is null ||
            !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var accessToken = jwtService.GenerateAccessToken(user);
        var refreshToken = jwtService.GenerateRefreshToken();

        user.RefreshTokenHash = jwtService.HashRefreshToken(refreshToken);
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);

        await context.SaveChangesAsync(cancellationToken);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            UserRoleId = user.UserRoleId,
            RoleName = user.UserRole.Name,
            OrganisationId = user.OrganisationId
        };
    }
}
