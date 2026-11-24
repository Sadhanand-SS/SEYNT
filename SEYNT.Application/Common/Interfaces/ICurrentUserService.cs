namespace SEYNT.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
    Guid OrganisationId { get; }
    string Role { get; }
    string UserRights { get; }
}
