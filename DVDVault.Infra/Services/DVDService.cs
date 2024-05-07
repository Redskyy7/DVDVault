using DVDVault.Domain.DTO;
using DVDVault.Domain.Interfaces.Services;
using DVDVault.Infra.Data.Context;
using DVDVault.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace DVDVault.Infra.Services;
public class DVDService : IDVDService
{
    private readonly DVDVaultContext _context;

    public DVDService(DVDVaultContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<DVDDTO>>> GetAllAsync()
    {
        try
        {
            var dvds = await _context.DVDs
                .AsNoTracking()
                .Select(x => new DVDDTO
                {
                    Id = x.Id,
                    Title = x.Title.Trim(),
                    Genre = x.Genre.Trim(),
                    Published = x.Published,
                    Copies = x.Copies,
                    Available = x.Available,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    DirectorId = x.DirectorId
                }).ToListAsync();

            return Result<IEnumerable<DVDDTO>>.Success(dvds);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task<Result<DVDDTO>> GetByIdAsync(int id)
    {
        try
        {
            var dvd = await _context.DVDs
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new DVDDTO
                {
                    Id = x.Id,
                    Title = x.Title.Trim(),
                    Genre = x.Genre.Trim(),
                    Published = x.Published,
                    Copies = x.Copies,
                    Available = x.Available,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    DirectorId = x.DirectorId
                }).FirstOrDefaultAsync();

            if (dvd is null)
                return Result<DVDDTO>.NotFound(System.Net.HttpStatusCode.NotFound, $"No DVD with id {id} found");

            return Result<DVDDTO>.Success(dvd);
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}
