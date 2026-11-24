namespace SEYNT.Domain.Entities;

public class UserRightEntity
{
    public string MenuName { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public bool View { get; set; }
    public bool Create { get; set; }
    public bool Edit { get; set; }
    public bool Delete { get; set; }

    // Grants the approval workflow on this controller.
    public bool Approve { get; set; }
}
