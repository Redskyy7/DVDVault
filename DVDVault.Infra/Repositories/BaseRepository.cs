using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Infra.Data.Context;
using DVDVault.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DVDVault.Infra.Repositories;
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
{
    private readonly DVDVaultContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DVDVaultContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAsync() 
        => await _dbSet.ToListAsync();

    public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

    public async Task AddAsync(TEntity entity)
        => await _dbSet.AddAsync(entity);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public void Delete(TEntity entity)
        => _dbSet.Remove(entity);
}
