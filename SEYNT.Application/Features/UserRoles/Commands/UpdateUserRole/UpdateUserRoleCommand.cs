using MediatR;

namespace SEYNT.Application.Features.UserRoles.Commands.UpdateUserRole;

public sealed class UpdateUserRoleCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserRights { get; set; } = "[]";
}
