using SEYNT.Domain.Common;

namespace SEYNT.Domain.Entities;

public class UserRoleEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string UserRights { get; set; } = "[]";

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
