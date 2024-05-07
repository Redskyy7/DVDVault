using DVDVault.Application.UseCases.DVDs.Request;
using FluentValidation;

namespace DVDVault.Application.Validators.DVD;
public class CreateDVDValidator : AbstractValidator<CreateDVDRequest>
{
    public CreateDVDValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage("Invalid title.")
            .Length(2, 120)
                .WithMessage("Title should have between 2 and 120 characters.");
        RuleFor(x => x.Genre)
            .NotEmpty()
                .WithMessage("Invalid genre.");
        RuleFor(x => x.Copies)
            .NotEmpty()
                .WithMessage("Invalid copies.")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Copies can't be negative");
        RuleFor(x => x.Published)
            .NotEmpty()
                .WithMessage("Invalid published.");
        RuleFor(x => x.DirectorId)
            .NotEmpty()
                .WithMessage("Invalid DirectorId");
    }
}
