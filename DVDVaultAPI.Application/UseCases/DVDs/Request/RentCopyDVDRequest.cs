using DVDVault.Application.Validators.DVD;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.DVDs.Request;
public class RentCopyDVDRequest : IRequest
{
    public RentCopyDVDRequest(int dvdId)
    {
        DVDId = dvdId;
    }

    public int DVDId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new RentCopyDVDValidator();
        return validator.Validate(this);
    }
}
