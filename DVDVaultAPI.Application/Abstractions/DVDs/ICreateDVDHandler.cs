using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.DVDs;
public interface ICreateDVDHandler
{
    Task<IResponse> Handle(CreateDVDRequest request, CancellationToken cancellationToken);
}
