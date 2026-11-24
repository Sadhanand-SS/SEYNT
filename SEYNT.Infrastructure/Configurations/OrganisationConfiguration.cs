using SEYNT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SEYNT.Infrastructure.Configurations;

public class OrganisationConfiguration : IEntityTypeConfiguration<OrganisationEntity>
{
    public void Configure(EntityTypeBuilder<OrganisationEntity> builder)
    {
        builder.ToTable("Organisations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrganisationName)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(x => x.OrganisationName)
            .IsUnique()
            .HasFilter("\"RecordStatus\" = 2");

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.Property(x => x.AdditionalFields)
            .IsRequired();

        builder.Property(x => x.ShipmentMilestoneLeadDays)
            .IsRequired()
            .HasDefaultValue(7);

        builder.Property(x => x.DrawbackLeadDays)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.RodtepEScriptGenLeadDays)
            .IsRequired()
            .HasDefaultValue(30);

        builder.Property(x => x.RodtepEScriptExpiryLeadDays)
            .IsRequired()
            .HasDefaultValue(180);

        builder.Property(x => x.DailyDigestTime)
            .IsRequired()
            .HasDefaultValue(new TimeOnly(7, 0));

        builder.Property(x => x.TimeZoneId)
            .IsRequired()
            .HasMaxLength(64)
            .HasDefaultValue("Asia/Kolkata");

        builder.Property(x => x.SmtpUser)
            .HasMaxLength(256);

        // Base64 of AES-GCM (nonce|tag|ciphertext).
        builder.Property(x => x.SmtpPasswordEncrypted)
            .HasMaxLength(512);

        builder.Property(x => x.SmtpHost)
            .HasMaxLength(256);

        builder.Property(x => x.RecordStatus)
            .HasConversion<byte>()
            .IsRequired();
    }
}
