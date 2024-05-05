using DVDVault.Domain.Interfaces.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using DVDVault.Infra.Data.Context;

namespace DVDVault.Infra.Repositories;
public sealed class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly DVDVaultContext _context;

    public UnitOfWork(DVDVaultContext dvdVaultContext)
    {
        _context = dvdVaultContext;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        _transaction?.Commit();
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }
}
