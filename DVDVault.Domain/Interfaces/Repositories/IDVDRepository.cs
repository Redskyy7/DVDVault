using DVDVault.Domain.Models;

namespace DVDVault.Domain.Interfaces.Repositories;
public interface IDVDRepository
{
    Task AddAsync(DVD dvd);
    Task<DVD> GetByIdAsync(int id);
    Task<bool> UpdateTitleAsync(int id, DVD dvd);
    public Task<bool> SoftDelete(int id, DVD dvd);
    public Task<bool> HardDelete(DVD dvd);

    //Task<bool> UpdateGenreAsync(int id, DVD dvd);
}
