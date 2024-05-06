using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Models;
using DVDVault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DVDVault.Infra.Repositories;
public sealed class DirectorRepository : IDirectorRepository
{
    private readonly DVDVaultContext _context;
    private readonly IBaseRepository<Director> _baseRepository;

    public DirectorRepository(IBaseRepository<Director> baseRepository, DVDVaultContext context)
    {
        _baseRepository = baseRepository;
        _context = context;
    }

    public Task AddAsync(Director director)
        => _baseRepository.AddAsync(director);

    public async Task<Director> GetByIdAsync(int id)
    {
        var result = await _context.Directors.FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
        {
            throw new InvalidOperationException($"Director with id {id} not found");
        }

        return result;
    }

    public async Task<bool> UpdateNameAsync(int id, Director director)
    {
        var updated = await _context
            .Directors
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => 
                            x.SetProperty(x => x.Name, director.Name)
                            .SetProperty(x => x.UpdatedAt, director.UpdatedAt));

        return updated != 0;
    }

    public async Task<bool> UpdateSurnameAsync(int id, Director director)
    {
        var updated = await _context
            .Directors
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x => 
                            x.SetProperty(x => x.Surname, director.Surname)
                            .SetProperty(x => x.UpdatedAt, director.UpdatedAt));

        return updated != 0;
    }

    public async Task<bool> SoftDelete(int id, Director director)
    {
        var updated = await _context
            .Directors
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x =>
                            x.SetProperty(x => x.IsActive, director.IsActive)
                            .SetProperty(x => x.DeletedAt, director.DeletedAt));

        return updated != 0;          
    }

    public async Task<bool> HardDelete(Director director)
    {
        var deleted = await _context
                            .Directors
                            .Where(x => x.Id == director.Id)
                            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}
