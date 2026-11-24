using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Entities;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Commands.CreateUserRole;

public sealed class CreateUserRoleCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<CreateUserRoleCommand, Guid>
{
    public async Task<Guid> Handle(
        CreateUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        var existingRole = await context.UserRoles
            .AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (existingRole)
            throw new ConflictException("User role with this name already exists.");

        var userRole = new UserRoleEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            UserRights = request.UserRights,
            CreatedBy = currentUserService.UserId,
            OrganisationId = currentUserService.OrganisationId,
            UpdatedBy = currentUserService.UserId,
        };

        context.UserRoles.Add(userRole);
        await context.SaveChangesAsync(cancellationToken);

        return userRole.Id;
    }
}
