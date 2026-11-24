using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Features.Organisations.Dtos;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Organisations.Queries.GetOrganisations;

// Organisations are the tenant boundary, not a tenant-scoped resource — this deliberately
// returns every active organisation and is NOT filtered by the caller's OrganisationId.
public sealed class GetOrganisationsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetOrganisationsQuery, List<GetOrganisationsResponseDto>>
{
    public async Task<List<GetOrganisationsResponseDto>> Handle(GetOrganisationsQuery request, CancellationToken cancellationToken)
    {
        return await context.Organisations
            .AsNoTracking()
            .Where(x => x.RecordStatus == RecordStatus.Active)
            .OrderBy(x => x.OrganisationName)
            .Select(x => new GetOrganisationsResponseDto
            {
                Id = x.Id,
                OrganisationName = x.OrganisationName,
                Email = x.Email,
                AdditionalFields = x.AdditionalFields,
                ShipmentMilestoneLeadDays = x.ShipmentMilestoneLeadDays,
                DrawbackLeadDays = x.DrawbackLeadDays,
                RodtepEScriptGenLeadDays = x.RodtepEScriptGenLeadDays,
                RodtepEScriptExpiryLeadDays = x.RodtepEScriptExpiryLeadDays,
                DailyDigestTime = x.DailyDigestTime,
                TimeZoneId = x.TimeZoneId,
                SmtpUser = x.SmtpUser,
                SmtpHost = x.SmtpHost,
                SmtpPort = x.SmtpPort,
                HasSmtpPassword = x.SmtpPasswordEncrypted != null,
                RecordStatus = (byte)x.RecordStatus,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedBy = x.UpdatedBy,
            })
            .ToListAsync(cancellationToken);
    }
}
