using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Organisations.Commands.UpdateOrganisation;

public sealed class UpdateOrganisationCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService,
    IEncryptionService encryptionService)
    : IRequestHandler<UpdateOrganisationCommand, Guid>
{
    public async Task<Guid> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisation = await context.Organisations
            .FirstOrDefaultAsync(
                x => x.Id == request.Id && x.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (organisation is null)
            throw new NotFoundException("Organisation not found.");

        var nameTaken = await context.Organisations
            .AnyAsync(
                x => x.Id != request.Id &&
                     x.OrganisationName == request.OrganisationName &&
                     x.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (nameTaken)
            throw new ConflictException("Organisation with this name already exists.");

        organisation.OrganisationName = request.OrganisationName;
        organisation.Email = request.Email;
        organisation.AdditionalFields = request.AdditionalFields;
        organisation.ShipmentMilestoneLeadDays = request.ShipmentMilestoneLeadDays;
        organisation.DrawbackLeadDays = request.DrawbackLeadDays;
        organisation.RodtepEScriptGenLeadDays = request.RodtepEScriptGenLeadDays;
        organisation.RodtepEScriptExpiryLeadDays = request.RodtepEScriptExpiryLeadDays;
        organisation.DailyDigestTime = request.DailyDigestTime;
        organisation.TimeZoneId = request.TimeZoneId;
        organisation.SmtpUser = request.SmtpUser;
        organisation.SmtpHost = request.SmtpHost;
        organisation.SmtpPort = request.SmtpPort;

        // Only replace the stored password when a new one is supplied.
        if (!string.IsNullOrWhiteSpace(request.SmtpPassword))
            organisation.SmtpPasswordEncrypted = encryptionService.Encrypt(request.SmtpPassword);

        organisation.UpdatedBy = currentUserService.UserId;

        await context.SaveChangesAsync(cancellationToken);

        return organisation.Id;
    }
}
