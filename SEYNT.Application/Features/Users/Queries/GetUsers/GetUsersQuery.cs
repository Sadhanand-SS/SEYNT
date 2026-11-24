using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.Users.Dtos;
using SEYNT.Domain.Enums;
using MediatR;

namespace SEYNT.Application.Features.Users.Queries.GetUsers;

public sealed class GetUsersQuery : IRequest<PaginatedResult<GetUsersResponseDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
    public byte? Status { get; set; } = (byte)RecordStatus.Active;
}
