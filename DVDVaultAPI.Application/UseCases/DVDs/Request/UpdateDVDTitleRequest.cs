using DVDVault.Application.Validators.DVD;
using DVDVault.Domain.Interfaces.Abstractions;
using FluentValidation.Results;

namespace DVDVault.Application.UseCases.DVDs.Request;
public class UpdateDVDTitleRequest : IRequest
{
    public UpdateDVDTitleRequest(int dvdId, string title)
    {
        DVDId = dvdId;
        Title = title;
    }

    public int DVDId { get; set; }
    public string Title { get; set; }

    public ValidationResult Validate()
    {
        var validator = new UpdateDVDTitleValidator();
        return validator.Validate(this);
    }
}
