using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.Director;
public interface ISoftDeleteDirectorHandler
{
    Task<IResponse> Handle(SoftDeleteDirectorRequest request, CancellationToken cancellationToken);
}
