using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Organisations.Commands.DeleteOrganisation;

public sealed class DeleteOrganisationCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<DeleteOrganisationCommand, Guid>
{
    public async Task<Guid> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisation = await context.Organisations
            .FirstOrDefaultAsync(
                x => x.Id == request.OrganisationId && x.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (organisation is null)
            throw new NotFoundException("Organisation not found.");

        // Guard against orphaning the tenant's own users.
        var hasActiveUsers = await context.Users
            .AnyAsync(
                x => x.OrganisationId == organisation.Id && x.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (hasActiveUsers)
            throw new BusinessRuleException("Organisation has active users and cannot be deleted.");

        organisation.RecordStatus = RecordStatus.Inactive;
        organisation.UpdatedBy = currentUserService.UserId;

        await context.SaveChangesAsync(cancellationToken);

        return organisation.Id;
    }
}
