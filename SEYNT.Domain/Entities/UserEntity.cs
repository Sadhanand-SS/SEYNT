using SEYNT.Domain.Common;

namespace SEYNT.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Guid UserRoleId { get; set; }
    public UserRoleEntity UserRole { get; set; } = null!;
    public string? RefreshTokenHash { get; set; }
    public DateTime? RefreshTokenExpiry { get; set; }
}
