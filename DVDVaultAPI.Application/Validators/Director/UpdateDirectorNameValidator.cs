using DVDVault.Application.UseCases.Directors.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.Director;
public class UpdateDirectorNameValidator : AbstractValidator<UpdateDirectorNameRequest>
{
    public UpdateDirectorNameValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Invalid Name.")
            .Length(2, 20)
                .WithMessage("Name should have between 2 and 20 characters.");
    }
}
