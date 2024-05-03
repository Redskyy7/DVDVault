using DVDVault.Domain.Models;

namespace DVDVault.Domain.Interfaces.Services;
public interface IDirectorService
{
    public string GetDirectorFullName(Director director);
}
