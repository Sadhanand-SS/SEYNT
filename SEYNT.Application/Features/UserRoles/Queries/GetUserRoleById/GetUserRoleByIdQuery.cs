using SEYNT.Application.Features.UserRoles.Dtos;
using MediatR;

namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRoleById;

public sealed class GetUserRoleByIdQuery : IRequest<GetUserRolesResponseDto>
{
    public Guid UserRoleId { get; set; }
}
