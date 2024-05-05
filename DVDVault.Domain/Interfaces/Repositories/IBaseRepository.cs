using System.Linq.Expressions;

namespace DVDVault.Domain.Interfaces.Repositories;
public interface IBaseRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAsync();
    Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
