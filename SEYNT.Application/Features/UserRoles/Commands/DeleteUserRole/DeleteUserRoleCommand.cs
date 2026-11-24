using MediatR;

namespace SEYNT.Application.Features.UserRoles.Commands.DeleteUserRole;

public sealed class DeleteUserRoleCommand : IRequest<Guid>
{
    public Guid UserRoleId { get; set; }
}
