using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Users.Queries.SearchUsers;

public sealed class SearchUsersQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<SearchUsersQuery, PaginatedResult<GetUsersResponseDto>>
{
    public async Task<PaginatedResult<GetUsersResponseDto>> Handle(
        SearchUsersQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Users
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
                x.FullName.ToLower().Contains(term) ||
                x.Email.ToLower().Contains(term));
        }

        if (request.RoleId.HasValue)
            query = query.Where(x => x.UserRoleId == request.RoleId.Value);

        if (request.FromDate.HasValue)
        {
            var toDate = request.ToDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
            query = query.Where(x =>
                DateOnly.FromDateTime(x.UpdatedAt ?? x.CreatedAt) >= request.FromDate.Value &&
                DateOnly.FromDateTime(x.UpdatedAt ?? x.CreatedAt) <= toDate);
        }
        else if (request.ToDate.HasValue)
        {
            query = query.Where(x =>
                DateOnly.FromDateTime(x.UpdatedAt ?? x.CreatedAt) <= request.ToDate.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .OrderBy(x => x.FullName)
            .ProjectTo<GetUsersResponseDto>(mapper.ConfigurationProvider)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<GetUsersResponseDto>(users, totalCount, request.Page, request.PageSize);
    }
}
