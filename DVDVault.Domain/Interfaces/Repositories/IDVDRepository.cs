using DVDVault.Domain.Models;

namespace DVDVault.Domain.Interfaces.Repositories;
public interface IDVDRepository
{
    Task AddAsync(DVD dvd);
}
