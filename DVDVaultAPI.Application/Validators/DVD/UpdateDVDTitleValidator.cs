using DVDVault.Application.UseCases.DVDs.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.DVD;
public class UpdateDVDTitleValidator : AbstractValidator<UpdateDVDTitleRequest>
{
    public UpdateDVDTitleValidator()
    {
        RuleFor(x => x.DVDId)
            .NotEmpty()
                .WithMessage("Invalid DVDId.");
        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage("Invalid Title.")
            .Length(2, 120)
                .WithMessage("Title should have between 2 and 120 characters.");
    }
}
