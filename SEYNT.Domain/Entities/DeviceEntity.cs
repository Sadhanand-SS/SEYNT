using SEYNT.Domain.Common;

namespace SEYNT.Domain.Entities;

public class DeviceEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = "Active";
}
