namespace SEYNT.Application.Features.Organisations.Dtos;

public sealed class GetOrganisationsResponseDto
{
    public Guid Id { get; set; }
    public string OrganisationName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string AdditionalFields { get; set; } = "{}";
    public int ShipmentMilestoneLeadDays { get; set; }
    public int DrawbackLeadDays { get; set; }
    public int RodtepEScriptGenLeadDays { get; set; }
    public int RodtepEScriptExpiryLeadDays { get; set; }
    public TimeOnly DailyDigestTime { get; set; }
    public string TimeZoneId { get; set; } = string.Empty;
    public string? SmtpUser { get; set; }
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }

    // Whether an SMTP password is configured — the encrypted secret itself is never returned.
    public bool HasSmtpPassword { get; set; }

    public byte RecordStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
