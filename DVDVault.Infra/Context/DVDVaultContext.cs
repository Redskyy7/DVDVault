using Microsoft.EntityFrameworkCore;
using DVDVault.Domain.Models;

namespace DVDVault.Infra.Context;
public class DVDVaultContext : DbContext
{
    public DVDVaultContext(DbContextOptions<DVDVaultContext> options) : base(options) { }

    public DbSet<DVD> DVDs { get; set; }

    public DbSet<Director> Directors { get; set; }
}
