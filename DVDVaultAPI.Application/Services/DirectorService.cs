using DVDVault.Domain.Models;
using DVDVault.Domain.Interfaces.Services;

namespace DVDVault.Application.Services;
public class DirectorService : IDirectorService
{
    public string GetDirectorFullName(Director director)
    {
        return $"{director.Name} {director.Surname}";
    }
}
