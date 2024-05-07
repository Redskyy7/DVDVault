using DVDVault.Application.Validators.DVD;
using DVDVault.Domain.Enums;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.DVDs.Request;
public class CreateDVDRequest : IRequest
{
    public CreateDVDRequest(string title, string genre, DateOnly published, int copies, int directorId)
    {
        Title = title;
        Genre = genre;
        Copies = copies;
        Published = published;
        DirectorId = directorId;
    }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public DateOnly Published { get; set; }

    public int Copies { get; set; }

    public int DirectorId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new CreateDVDValidator();
        return validator.Validate(this);
    }

    private GenreEnum ParseGenre(string genre)
    {
        if (!Enum.TryParse<GenreEnum>(genre, true, out var parsedGenre))
        {
            throw new ArgumentException($"Invalid genre: {genre}", nameof(genre));
        }
        return parsedGenre;
    }
}
