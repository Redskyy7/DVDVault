using DVDVault.Application.Validators.Director;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.Directors.Request;
public class CreateDirectorRequest : IRequest
{
    public CreateDirectorRequest()
    {
        Name = Name;
        Surname = Surname;
    }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public ValidationResult Validate()
    {
        var validator = new CreateDirectorValidator();
        return validator.Validate(this);
    }
}
