using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.DVDs;
public interface IUpdateDVDTitleHandler
{
    Task<IResponse> Handle(UpdateDVDTitleRequest request, CancellationToken cancellationToken);
}
