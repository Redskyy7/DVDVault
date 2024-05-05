namespace DVDVault.Domain.Interfaces.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    Task Commit(CancellationToken cancellationToken);
    void Rollback();
}
