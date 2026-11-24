using SEYNT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SEYNT.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(200);

        // citext = case-insensitive text: rejects case-variant duplicates and login matches
        // regardless of case. Requires the "citext" extension (enabled in OnModelCreating).
        builder.Property(u => u.Email)
            .IsRequired()
            .HasColumnType("citext")
            .HasMaxLength(256);

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("\"RecordStatus\" = 2");

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasOne(u => u.UserRole)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.UserRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(u => u.RecordStatus)
            .HasConversion<byte>()
            .IsRequired();

        builder.Property(u => u.RefreshTokenHash)
            .HasMaxLength(512);

        builder.Property(x => x.OrganisationId)
            .IsRequired();

        builder.HasIndex(x => x.OrganisationId);

        builder.HasOne<OrganisationEntity>()
            .WithMany()
            .HasForeignKey(x => x.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
