using SEYNT.Domain.Enums;

namespace SEYNT.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    public RecordStatus RecordStatus { get; set; } = RecordStatus.Active;

    // Every row belongs to exactly one organisation (required FK, configured in each
    // entity configuration). OrganisationEntity itself does NOT inherit BaseEntity.
    public Guid OrganisationId { get; set; }
}
