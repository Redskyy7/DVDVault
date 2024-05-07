using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Models;
using DVDVault.Infra.Data.Context;

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
}
