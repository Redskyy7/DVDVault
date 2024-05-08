using DVDVault.Application.UseCases.DVDs.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.DVD;
public class RentCopyDVDValidator : AbstractValidator<RentCopyDVDRequest>
{
    public RentCopyDVDValidator()
    {
        RuleFor(x => x.DVDId)
            .NotEmpty()
                .WithMessage("Invalid DVDId.");
    }
}
