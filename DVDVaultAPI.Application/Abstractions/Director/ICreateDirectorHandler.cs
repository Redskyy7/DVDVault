using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.Director;
public interface ICreateDirectorHandler
{
    Task<IResponse> Handle(CreateDirectorRequest request, CancellationToken cancellationToken);
}
