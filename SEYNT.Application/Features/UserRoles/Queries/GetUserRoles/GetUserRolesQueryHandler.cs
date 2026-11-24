using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.UserRoles.Dtos;
using SEYNT.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRoles;

public sealed class GetUserRolesQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetUserRolesQuery, PaginatedResult<GetUserRolesResponseDto>>
{
    public async Task<PaginatedResult<GetUserRolesResponseDto>> Handle(
        GetUserRolesQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.UserRoles
            .AsNoTracking()
            .Where(x => x.RecordStatus == (RecordStatus)request.Status! && x.OrganisationId == currentUserService.OrganisationId);

        var totalCount = await query.CountAsync(cancellationToken);

        var roles = await query
            .OrderByDescending(x => x.UpdatedAt ?? x.CreatedAt)
            .ProjectTo<GetUserRolesResponseDto>(mapper.ConfigurationProvider)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<GetUserRolesResponseDto>(roles, totalCount, request.Page, request.PageSize);
    }
}
