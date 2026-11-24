using SEYNT.Application.Features.Auth.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
