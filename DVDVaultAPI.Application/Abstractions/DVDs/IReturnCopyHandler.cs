using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.DVDs;
public interface IReturnCopyHandler
{
    Task<IResponse> Handle(ReturnCopyDVDRequest request, CancellationToken cancellationToken);
}
