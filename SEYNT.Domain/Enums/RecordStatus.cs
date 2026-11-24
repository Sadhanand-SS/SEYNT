namespace SEYNT.Domain.Enums;

public enum RecordStatus : byte
{
    // Created but not yet approved. Editable by its creator, invisible to reports and to
    // every "usable data" query. An aggregate root and its whole child graph sit here
    // until an approver moves them to Active. Also the state a Rejected root stays in.
    Draft = 1,

    // Approved and usable — the only status reports and options lists consider.
    Active = 2,

    // Soft deleted.
    Inactive = 4
}
