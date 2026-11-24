using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Features.UserRoles.Queries.GetUserRolesOptionsList.Dtos;
using SEYNT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRolesOptionsList;

public sealed class GetUserRolesOptionsListQueryHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetUserRolesOptionsListQuery, List<UserRoleOptionDto>>
{
    public async Task<List<UserRoleOptionDto>> Handle(
        GetUserRolesOptionsListQuery request,
        CancellationToken cancellationToken)
    {
        return await context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.RecordStatus == RecordStatus.Active &&
                         ur.OrganisationId == currentUserService.OrganisationId)
            .OrderByDescending(ur => ur.UpdatedAt ?? ur.CreatedAt)
            .Select(ur => new UserRoleOptionDto
            {
                Id = ur.Id,
                OrganisationId = ur.OrganisationId,
                Name = ur.Name,
                Description = ur.Description,
            })
            .ToListAsync(cancellationToken);
    }
}
