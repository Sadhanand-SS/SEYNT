namespace SEYNT.Application.Features.Users.Dtos;

public class CreateUserResponseDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public Guid UserRoleId { get; set; }
}
