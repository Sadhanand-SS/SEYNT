namespace SEYNT.Application.Features.UserRoles.Queries.GetUserRolesOptionsList.Dtos;

public sealed class UserRoleOptionDto
{
    public Guid Id { get; set; }
    public Guid OrganisationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
