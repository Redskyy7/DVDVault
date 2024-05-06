using DVDVault.Application.Validators.Director;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.Directors.Request;
public class UpdateDirectorNameRequest : IRequest
{
    public UpdateDirectorNameRequest(int directorId, string name)
    {
        DirectorId = directorId;
        Name = name;
    }
    public int DirectorId { get; set; }
    public string Name { get; set; }

    public ValidationResult Validate()
    {
        var validator = new UpdateDirectorNameValidator();
        return validator.Validate(this);
    }
}
