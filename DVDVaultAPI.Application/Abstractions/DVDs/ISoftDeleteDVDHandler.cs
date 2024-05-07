using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.DVDs;
public interface ISoftDeleteDVDHandler
{
    Task<IResponse> Handle(SoftDeleteDVDRequest request, CancellationToken cancellationToken);
}
