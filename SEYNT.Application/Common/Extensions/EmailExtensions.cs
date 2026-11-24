namespace SEYNT.Application.Common.Extensions;

public static class EmailExtensions
{
    /// <summary>
    /// Canonical form used for storing and comparing emails: trimmed and lower-cased.
    /// Emails are treated case-insensitively, so "User@example.com" and
    /// "user@example.com" normalise to the same value.
    /// </summary>
    public static string NormalizeEmail(this string? email) =>
        (email ?? string.Empty).Trim().ToLowerInvariant();
}
