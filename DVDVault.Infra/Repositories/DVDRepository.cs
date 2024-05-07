using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Models;
using DVDVault.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DVDVault.Infra.Repositories;
public sealed class DVDRepository : IDVDRepository
{
    private readonly DVDVaultContext _context;
    private readonly IBaseRepository<DVD> _baseRepository;

    public DVDRepository(IBaseRepository<DVD> baseRepository, DVDVaultContext context)
    {
        _baseRepository = baseRepository;
        _context = context;
    }

    public Task AddAsync(DVD dvd)
        => _baseRepository.AddAsync(dvd);

    public async Task<DVD> GetByIdAsync(int id)
    {
        var result = await _context.DVDs.FirstOrDefaultAsync(d => d.Id == id);

        if (result is null) 
        {
            throw new InvalidOperationException($"DVD with id {id} not found");
        }

        return result;
    }

    public async Task<bool> UpdateTitleAsync(int id, DVD dvd)
    {
        var updated = await _context
            .DVDs
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x =>
                            x.SetProperty(x => x.Title, dvd.Title)
                            .SetProperty(x => x.UpdatedAt, dvd.UpdatedAt));

        return updated != 0;
    }
    public async Task<bool> SoftDelete(int id, DVD dvd)
    {
        var updated = await _context
            .DVDs
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(x =>
                            x.SetProperty(x => x.Available, dvd.Available)
                            .SetProperty(x => x.DeletedAt, dvd.DeletedAt));

        return updated != 0;
    }

    public async Task<bool> HardDelete(DVD dvd)
    {
        var deleted = await _context
                            .DVDs
                            .Where(x => x.Id == dvd.Id)
                            .ExecuteDeleteAsync();

        return deleted != 0;
    }
}
