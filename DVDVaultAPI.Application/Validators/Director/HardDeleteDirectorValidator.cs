using DVDVault.Application.UseCases.Directors.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.Director;
public class HardDeleteDirectorValidator : AbstractValidator<HardDeleteDirectorRequest>
{
    public HardDeleteDirectorValidator()
    {
        RuleFor(x => x.DirectorId)
            .NotEmpty()
                .WithMessage("Provided Id is not valid.");
    }
}
