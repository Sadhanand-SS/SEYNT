using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.UserRoles.Dtos;
using MediatR;

namespace SEYNT.Application.Features.UserRoles.Queries.SearchUserRoles;

public sealed class SearchUserRolesQuery : IRequest<PaginatedResult<GetUserRolesResponseDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? SearchTerm { get; set; }
    public byte? Status { get; set; }
}
