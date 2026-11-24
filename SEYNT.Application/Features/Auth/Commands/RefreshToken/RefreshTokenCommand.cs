using SEYNT.Application.Features.Auth.Dtos;
using MediatR;

namespace SEYNT.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponseDto>
{
    public string RefreshToken { get; set; } = string.Empty;
}
