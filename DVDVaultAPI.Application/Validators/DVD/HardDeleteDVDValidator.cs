using DVDVault.Application.UseCases.DVDs.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.DVD;
public class HardDeleteDVDValidator : AbstractValidator<HardDeleteDVDRequest>
{
    public HardDeleteDVDValidator()
    {
        RuleFor(x => x.DVDId)
            .NotEmpty()
                .WithMessage("Provided Id is not valid.");
    }
}
