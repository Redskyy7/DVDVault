using DVDVault.Application.Validators.Director;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.Directors.Request;
public class HardDeleteDirectorRequest : IRequest
{
    public HardDeleteDirectorRequest(int directorId)
    {
        DirectorId = directorId;
    }

    public int DirectorId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new HardDeleteDirectorValidator();
        return validator.Validate(this);
    }
}
