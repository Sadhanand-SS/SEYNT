namespace SEYNT.Infrastructure.Security;

public class EncryptionSettings
{
    public const string SectionName = "Encryption";

    public string Key { get; set; } = string.Empty;
}
