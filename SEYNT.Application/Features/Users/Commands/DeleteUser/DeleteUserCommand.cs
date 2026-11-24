using MediatR;

namespace SEYNT.Application.Features.Users.Commands.DeleteUser;

public sealed class DeleteUserCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
}
