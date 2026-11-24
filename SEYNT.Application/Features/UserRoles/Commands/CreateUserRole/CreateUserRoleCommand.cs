using MediatR;

namespace SEYNT.Application.Features.UserRoles.Commands.CreateUserRole;

public sealed class CreateUserRoleCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserRights { get; set; } = "[]";
}
