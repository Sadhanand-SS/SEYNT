using SEYNT.Domain.Entities;

namespace SEYNT.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(UserEntity user);
    string GenerateRefreshToken();
    string HashRefreshToken(string refreshToken);
}
