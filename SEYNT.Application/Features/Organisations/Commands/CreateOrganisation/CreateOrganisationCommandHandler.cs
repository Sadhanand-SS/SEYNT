using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Exceptions;
using SEYNT.Domain.Entities;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Organisations.Commands.CreateOrganisation;

// OrganisationEntity is the tenant root — it does NOT inherit BaseEntity and carries no
// OrganisationId, so (unlike every other create handler) nothing is stamped from the
// caller's organisation. CreatedAt/UpdatedAt are stamped centrally in SaveChangesAsync.
public sealed class CreateOrganisationCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService,
    IEncryptionService encryptionService)
    : IRequestHandler<CreateOrganisationCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var nameTaken = await context.Organisations
            .AnyAsync(
                x => x.OrganisationName == request.OrganisationName &&
                     x.RecordStatus == RecordStatus.Active,
                cancellationToken);

        if (nameTaken)
            throw new ConflictException("Organisation with this name already exists.");

        var organisation = new OrganisationEntity
        {
            Id = Guid.NewGuid(),
            OrganisationName = request.OrganisationName,
            Email = request.Email,
            AdditionalFields = request.AdditionalFields,
            ShipmentMilestoneLeadDays = request.ShipmentMilestoneLeadDays,
            DrawbackLeadDays = request.DrawbackLeadDays,
            RodtepEScriptGenLeadDays = request.RodtepEScriptGenLeadDays,
            RodtepEScriptExpiryLeadDays = request.RodtepEScriptExpiryLeadDays,
            DailyDigestTime = request.DailyDigestTime,
            TimeZoneId = request.TimeZoneId,
            SmtpUser = request.SmtpUser,
            SmtpPasswordEncrypted = string.IsNullOrWhiteSpace(request.SmtpPassword)
                ? null
                : encryptionService.Encrypt(request.SmtpPassword),
            SmtpHost = request.SmtpHost,
            SmtpPort = request.SmtpPort,
            RecordStatus = RecordStatus.Active,
            CreatedBy = currentUserService.UserId,
            UpdatedBy = currentUserService.UserId,
        };

        context.Organisations.Add(organisation);
        await context.SaveChangesAsync(cancellationToken);

        return organisation.Id;
    }
}
