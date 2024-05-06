using DVDVault.Application.UseCases.Directors.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.Director;
public class UpdateDirectorSurnameValidator : AbstractValidator<UpdateDirectorSurnameRequest>
{
    public UpdateDirectorSurnameValidator()
    {
        RuleFor(x => x.Surname)
            .NotEmpty()
                .WithMessage("Invalid Surname.")
            .Length(2, 100)
                .WithMessage("Surname should have between 2 and 100 characters.");
    }
}
