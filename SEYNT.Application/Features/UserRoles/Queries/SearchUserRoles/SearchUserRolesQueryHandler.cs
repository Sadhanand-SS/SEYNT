using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.UserRoles.Dtos;
using SEYNT.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.UserRoles.Queries.SearchUserRoles;

public sealed class SearchUserRolesQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<SearchUserRolesQuery, PaginatedResult<GetUserRolesResponseDto>>
{
    public async Task<PaginatedResult<GetUserRolesResponseDto>> Handle(
        SearchUserRolesQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.UserRoles
            .AsNoTracking()
            .Where(x => x.OrganisationId == currentUserService.OrganisationId);

        if (request.Status.HasValue)
        {
            var status = (RecordStatus)request.Status.Value;
            query = query.Where(x => x.RecordStatus == status);
        }
        else
        {
            query = query.Where(x => x.RecordStatus == RecordStatus.Active);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.Trim().ToLower();
            query = query.Where(x =>
                x.Name.ToLower().Contains(term) ||
                x.Description.ToLower().Contains(term));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var roles = await query
            .OrderBy(x => x.Name)
            .ProjectTo<GetUserRolesResponseDto>(mapper.ConfigurationProvider)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<GetUserRolesResponseDto>(roles, totalCount, request.Page, request.PageSize);
    }
}
