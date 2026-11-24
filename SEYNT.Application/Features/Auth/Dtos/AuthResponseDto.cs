namespace SEYNT.Application.Features.Auth.Dtos;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public Guid UserRoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public Guid OrganisationId { get; set; }
}
