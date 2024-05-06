using DVDVault.Application.Validators.Director;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.Directors.Request;
public class UpdateDirectorSurnameRequest : IRequest
{
    public UpdateDirectorSurnameRequest(int directorId, string surname)
    {
        DirectorId = directorId;
        Surname = surname;
    }

    public int DirectorId { get; set; } 
    public string Surname { get; set; }

    public ValidationResult Validate()
    {
        var validator = new UpdateDirectorSurnameValidator();
        return validator.Validate(this);
    }
}
