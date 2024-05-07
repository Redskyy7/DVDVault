using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.DVDs;
public interface IHardDeleteDVDHandler
{
    Task<IResponse> Handle(HardDeleteDVDRequest request, CancellationToken cancellationToken);
}
