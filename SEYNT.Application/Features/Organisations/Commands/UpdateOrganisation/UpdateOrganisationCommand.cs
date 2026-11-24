using MediatR;

namespace SEYNT.Application.Features.Organisations.Commands.UpdateOrganisation;

public sealed class UpdateOrganisationCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string OrganisationName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string AdditionalFields { get; set; } = "{}";
    public int ShipmentMilestoneLeadDays { get; set; } = 7;
    public int DrawbackLeadDays { get; set; }
    public int RodtepEScriptGenLeadDays { get; set; } = 30;
    public int RodtepEScriptExpiryLeadDays { get; set; } = 180;
    public TimeOnly DailyDigestTime { get; set; } = new(7, 0);
    public string TimeZoneId { get; set; } = "Asia/Kolkata";

    // Leave SmtpPassword null/blank to keep the existing stored password;
    // provide a value to replace it (encrypted by the handler).
    public string? SmtpUser { get; set; }
    public string? SmtpPassword { get; set; }
    public string? SmtpHost { get; set; }
    public int? SmtpPort { get; set; }
}
