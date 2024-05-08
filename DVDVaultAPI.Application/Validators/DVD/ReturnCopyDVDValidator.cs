using DVDVault.Application.UseCases.DVDs.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.DVD;
public class ReturnCopyDVDValidator : AbstractValidator<ReturnCopyDVDRequest>
{
    public ReturnCopyDVDValidator()
    {
        RuleFor(x => x.DVDId)
            .NotEmpty()
                .WithMessage("Invalid DVDId.");
    }
}
