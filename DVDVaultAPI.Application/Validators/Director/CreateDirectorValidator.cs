using DVDVault.Application.UseCases.Directors.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.Director;
public class CreateDirectorValidator : AbstractValidator<CreateDirectorRequest>
{
    public CreateDirectorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Invalid name")
            .Length(2, 20)
                .WithMessage("Name should have between 2 and 20 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty()
                .WithMessage("Invalid surname")
            .Length(5, 100)
                .WithMessage("Surname should have between 5 and 100 characters.");
    }
}
