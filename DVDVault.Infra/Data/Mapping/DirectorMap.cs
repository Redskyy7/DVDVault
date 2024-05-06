using DVDVault.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DVDVault.Infra.Data.Mapping;
public sealed class DirectorMap : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.ToTable("Directors");

        builder.Ignore(x => x.Guid);
        builder.Ignore(x => x.IsValid);
        builder.Ignore(x => x.Errors);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);

        builder.Property(x => x.IsActive)
            .HasColumnName("IsActive")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("char(20)")
            .IsRequired();

        builder.Property(x => x.Surname)
            .HasColumnName("Surname")
            .HasColumnType("char(100)")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("timestamp")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("timestamp");

        builder.Property(x => x.DeletedAt)
            .HasColumnName("DeletedAt")
            .HasColumnType("timestamp");

        builder.HasMany(x => x.DVDs)
            .WithOne(x => x.Director)
            .HasForeignKey(x => x.DirectorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
