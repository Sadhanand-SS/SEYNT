using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Commands.DeleteUserRole;

public sealed class DeleteUserRoleCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteUserRoleCommand, Guid>
{
    public async Task<Guid> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var roleToDelete = await context.UserRoles
            .FirstOrDefaultAsync(
                x => x.Id == request.UserRoleId &&
                     x.RecordStatus == RecordStatus.Active &&
                     x.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (roleToDelete is null)
            throw new NotFoundException("User role not found.");

        roleToDelete.RecordStatus = RecordStatus.Inactive;
        roleToDelete.UpdatedBy = currentUserService.UserId;

        await context.SaveChangesAsync(cancellationToken);

        return roleToDelete.Id;
    }
}
