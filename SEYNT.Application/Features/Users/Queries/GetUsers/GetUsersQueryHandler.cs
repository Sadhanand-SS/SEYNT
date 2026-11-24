using SEYNT.Application.Common.Interfaces;
using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Domain.Enums;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace SEYNT.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(
    IApplicationDbContext context,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetUsersQuery, PaginatedResult<GetUsersResponseDto>>
{
    public async Task<PaginatedResult<GetUsersResponseDto>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var query = context.Users
            .AsNoTracking()
            .Where(u => u.RecordStatus == (RecordStatus)request.Status! && u.OrganisationId == currentUserService.OrganisationId)
            .OrderByDescending(u => u.UpdatedAt)
            .ProjectTo<GetUsersResponseDto>(mapper.ConfigurationProvider);

        var totalCount = await query.CountAsync(cancellationToken);

        var users = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<GetUsersResponseDto>(users, totalCount, request.Page, request.PageSize);
    }
}
