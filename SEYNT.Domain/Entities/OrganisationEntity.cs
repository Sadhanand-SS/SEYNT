using SEYNT.Domain.Enums;

namespace SEYNT.Domain.Entities;

// Deliberately does NOT inherit BaseEntity: BaseEntity carries OrganisationId (every row
// belongs to an organisation), which would be self-referential here. The audit fields are
// copied instead.
public sealed class OrganisationEntity
{
    // Seeded by the AddOrganisation migration; used as the fallback OrganisationId for
    // rows created before real per-request organisation context is wired in.
    public static readonly Guid DefaultOrganisationId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    public RecordStatus RecordStatus { get; set; } = RecordStatus.Active;

    public string OrganisationName { get; set; } = string.Empty;

    public string? Email { get; set; }

    // Free-form JSON blob for org-specific settings that don't warrant columns yet.
    public string AdditionalFields { get; set; } = "{}";

    // ---- Notification lead days (per organisation) ----
    public int ShipmentMilestoneLeadDays { get; set; } = 7;
    public int DrawbackLeadDays { get; set; }
    public int RodtepEScriptGenLeadDays { get; set; } = 30;
    public int RodtepEScriptExpiryLeadDays { get; set; } = 180;

    // Wall-clock local time (in TimeZoneId) at which this org's daily digest is sent.
    public TimeOnly DailyDigestTime { get; set; } = new(7, 0);

    // IANA time zone the DailyDigestTime is expressed in (e.g. "Asia/Kolkata").
    public string TimeZoneId { get; set; } = "Asia/Kolkata";

    // Per-organisation SMTP sender.
    public string? SmtpUser { get; set; }
    public string? SmtpPasswordEncrypted { get; set; }
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
}
