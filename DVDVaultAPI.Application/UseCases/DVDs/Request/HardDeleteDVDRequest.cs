using DVDVault.Application.Validators.DVD;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.DVDs.Request;
public class HardDeleteDVDRequest : IRequest
{
    public HardDeleteDVDRequest(int dvdId)
    {
        DVDId = dvdId;
    }

    public int DVDId { get; set; }

    public ValidationResult Validate()
    {
        var validator = new HardDeleteDVDValidator();
        return validator.Validate(this);
    }
}
