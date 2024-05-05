using Microsoft.EntityFrameworkCore;
using DVDVault.Domain.Models;
using DVDVault.Shared.Entities;

namespace DVDVault.Infra.Data.Context;
public class DVDVaultContext : DbContext
{
    public DVDVaultContext(DbContextOptions<DVDVaultContext> options) : base(options) { }

    public DbSet<DVD> DVDs { get; set; }

    public DbSet<Director> Directors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Entity>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DVDVaultContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
