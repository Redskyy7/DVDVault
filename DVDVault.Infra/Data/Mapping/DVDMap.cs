using DVDVault.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DVDVault.Infra.Data.Mapping;
public sealed class DVDMap: IEntityTypeConfiguration<DVD>
{
    public void Configure(EntityTypeBuilder<DVD> builder)
    {
        builder.ToTable("DVDs");

        builder.Ignore(x => x.Guid);
        builder.Ignore(x => x.IsValid);
        builder.Ignore(x => x.Errors);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);

        builder.Property(x => x.Copies)
            .HasColumnName("Copies")
            .HasColumnType("integer")
            .IsRequired();

        builder.Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("char(75)")
            .IsRequired();

        builder.Property(x => x.Genre)
            .HasColumnName("Genre")
            .HasColumnType("char(40)")
            .IsRequired();

        builder.Property(x => x.Published)
            .HasColumnName("Published")
            .HasColumnType("date")
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

        builder.HasOne(dvd => dvd.Director)
            .WithMany(director => director.DVDs)
            .HasForeignKey(dvd => dvd.DirectorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
