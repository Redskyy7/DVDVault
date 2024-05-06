using DVDVault.Application.Validators.Director;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.Directors.Request;
public class SoftDeleteDirectorRequest : IRequest
{
    public SoftDeleteDirectorRequest(int directorId)
    {
        DirectorId = directorId;
    }

    public int DirectorId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new SoftDeleteDirectorValidator();
        return validator.Validate(this);
    }
}
