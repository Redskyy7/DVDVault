using DVDVault.Application.UseCases.DVDs.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.DVD;
public class SoftDeleteDVDValidator : AbstractValidator<SoftDeleteDVDRequest>
{
    public SoftDeleteDVDValidator()
    {
        RuleFor(x => x.DVDId)
            .NotEmpty()
                .WithMessage("Provided Id is not valid.");
    }
}
