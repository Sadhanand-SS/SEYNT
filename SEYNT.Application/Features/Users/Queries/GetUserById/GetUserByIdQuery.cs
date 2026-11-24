using SEYNT.Application.Features.Users.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdQuery : IRequest<GetUsersResponseDto>
{
    public Guid UserId { get; set; }
}
