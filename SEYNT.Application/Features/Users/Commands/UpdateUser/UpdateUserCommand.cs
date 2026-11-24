using MediatR;

namespace SEYNT.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public Guid UserRoleId { get; set; }
}
