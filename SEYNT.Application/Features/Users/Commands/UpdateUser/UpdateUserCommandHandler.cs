using SEYNT.Application.Common.Extensions;
using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateUserCommand, Guid>
{
    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     x.RecordStatus == RecordStatus.Active &&
                     x.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (user is null)
            throw new NotFoundException("User not found.");

        var roleExists = await context.UserRoles
            .AnyAsync(
                r => r.Id == request.UserRoleId &&
                     r.RecordStatus == RecordStatus.Active &&
                     r.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (!roleExists)
            throw new NotFoundException("User role not found.");

        var email = request.Email.NormalizeEmail();

        var emailTaken = await context.Users
            .AnyAsync(
                u => u.Id != request.Id && u.Email.ToLower() == email,
                cancellationToken);

        if (emailTaken)
            throw new ConflictException("User with this email already exists.");

        user.FullName = request.FullName;
        user.Email = email;
        user.UserRoleId = request.UserRoleId;

        if (!string.IsNullOrWhiteSpace(request.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        user.UpdatedBy = currentUserService.UserId;

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
