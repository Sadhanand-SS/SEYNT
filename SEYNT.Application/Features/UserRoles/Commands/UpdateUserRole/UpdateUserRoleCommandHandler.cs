using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Commands.UpdateUserRole;

public sealed class UpdateUserRoleCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateUserRoleCommand, Guid>
{
    public async Task<Guid> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var roleToUpdate = await context.UserRoles
            .FirstOrDefaultAsync(
                x => x.Id == request.Id &&
                     x.RecordStatus == RecordStatus.Active &&
                     x.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (roleToUpdate is null)
            throw new NotFoundException("User role not found.");

        var nameTaken = await context.UserRoles
            .AnyAsync(x => x.Id != request.Id && x.Name == request.Name, cancellationToken);

        if (nameTaken)
            throw new ConflictException("User role with this name already exists.");

        roleToUpdate.Name = request.Name;
        roleToUpdate.Description = request.Description;
        roleToUpdate.UserRights = request.UserRights;
        roleToUpdate.UpdatedBy = currentUserService.UserId;

        await context.SaveChangesAsync(cancellationToken);

        return roleToUpdate.Id;
    }
}
