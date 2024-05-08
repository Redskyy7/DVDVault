using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.DVDs;
public interface IRentCopyHandler
{
    Task<IResponse> Handle(RentCopyDVDRequest request, CancellationToken cancellationToken);
}
