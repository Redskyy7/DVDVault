using DVDVault.Domain.DTO;
using DVDVault.Shared.Results;

namespace DVDVault.Domain.Interfaces.Services;
public interface IDVDService
{
    Task<Result<IEnumerable<DVDDTO>>> GetAllAsync();
    Task<Result<DVDDTO>> GetByIdAsync(int id);
}
