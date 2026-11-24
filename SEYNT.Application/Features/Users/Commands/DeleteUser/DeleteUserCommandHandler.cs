using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteUserCommand, Guid>
{
    public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userToDelete = await context.Users
            .FirstOrDefaultAsync(
                x => x.Id == request.UserId &&
                     x.RecordStatus == RecordStatus.Active &&
                     x.OrganisationId == currentUserService.OrganisationId,
                cancellationToken);

        if (userToDelete is null)
            throw new NotFoundException("User not found.");

        userToDelete.RecordStatus = RecordStatus.Inactive;
        userToDelete.UpdatedBy = currentUserService.UserId;

        await context.SaveChangesAsync(cancellationToken);

        return userToDelete.Id;
    }
}
