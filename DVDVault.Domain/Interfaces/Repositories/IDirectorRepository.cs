using DVDVault.Domain.Models;

namespace DVDVault.Domain.Interfaces.Repositories;
public interface IDirectorRepository
{
    Task AddAsync(Director director);

    Task<Director> GetByIdAsync(int id);
}
