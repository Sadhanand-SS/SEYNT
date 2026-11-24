using MediatR;

namespace SEYNT.Application.Features.Organisations.Commands.CreateOrganisation;

public sealed class CreateOrganisationCommand : IRequest<Guid>
{
    public string OrganisationName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string AdditionalFields { get; set; } = "{}";
    public int ShipmentMilestoneLeadDays { get; set; } = 7;
    public int DrawbackLeadDays { get; set; }
    public int RodtepEScriptGenLeadDays { get; set; } = 30;
    public int RodtepEScriptExpiryLeadDays { get; set; } = 180;
    public TimeOnly DailyDigestTime { get; set; } = new(7, 0);
    public string TimeZoneId { get; set; } = "Asia/Kolkata";

    // Per-org SMTP sender. SmtpPassword is plaintext on the way in; the handler encrypts it
    // before persisting. Host/Port optional (default to the global SmtpSettings).
    public string? SmtpUser { get; set; }
    public string? SmtpPassword { get; set; }
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
}
