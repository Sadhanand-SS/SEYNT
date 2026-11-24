using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.UserRoles.Dtos;
using SEYNT.Domain.Enums;
using MediatR;

namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRoles;

public sealed class GetUserRolesQuery : IRequest<PaginatedResult<GetUserRolesResponseDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public byte? Status { get; set; } = (byte)RecordStatus.Active;
}
