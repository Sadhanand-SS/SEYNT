namespace SEYNT.Application.Features.Users.Dtos;

public sealed class GetUsersResponseDto
{
    public Guid Id { get; set; }
    public Guid OrganisationId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Guid UserRoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public byte RecordStatus { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
