using SEYNT.Application.Common.Models;
using SEYNT.Application.Features.Users.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Users.Queries.SearchUsers;

public sealed class SearchUsersQuery : IRequest<PaginatedResult<GetUsersResponseDto>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
    public string? SearchTerm { get; set; }
    public Guid? RoleId { get; set; }
    public byte? Status { get; set; }
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
}
