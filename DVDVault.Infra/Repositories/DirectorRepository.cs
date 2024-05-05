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
}
