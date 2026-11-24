using SEYNT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SEYNT.Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(r => r.Name)
            .IsUnique()
            .HasFilter("\"RecordStatus\" = 2");

        builder.Property(r => r.Description)
            .HasMaxLength(500);

        builder.Property(r => r.UserRights)
            .IsRequired();

        builder.Property(r => r.RecordStatus)
            .HasConversion<byte>()
            .IsRequired();

        builder.Property(x => x.OrganisationId)
            .IsRequired();

        builder.HasIndex(x => x.OrganisationId);

        builder.HasOne<OrganisationEntity>()
            .WithMany()
            .HasForeignKey(x => x.OrganisationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
