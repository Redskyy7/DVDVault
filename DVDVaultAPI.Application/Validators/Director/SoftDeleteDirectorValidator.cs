using DVDVault.Application.UseCases.Directors.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.Director;
public class SoftDeleteDirectorValidator : AbstractValidator<SoftDeleteDirectorRequest>
{
    public SoftDeleteDirectorValidator()
    {
        RuleFor(x => x.DirectorId)
            .NotEmpty()
                .WithMessage("Provided Id is not valid.");  
    }
}
