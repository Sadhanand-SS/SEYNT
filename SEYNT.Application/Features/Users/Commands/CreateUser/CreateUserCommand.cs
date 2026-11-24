using SEYNT.Application.Features.Users.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<CreateUserResponseDto>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid UserRoleId { get; set; }
}
