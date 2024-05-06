using DVDVault.Domain.Models;

namespace DVDVault.Domain.Interfaces.Repositories;
public interface IDirectorRepository
{
    Task AddAsync(Director director);

    Task<Director> GetByIdAsync(int id);

    Task<bool> UpdateNameAsync(int id, Director director);

    Task<bool> UpdateSurnameAsync(int id, Director director);

    Task<bool> SoftDelete(int id, Director director);

    Task<bool> HardDelete(Director director);
}
