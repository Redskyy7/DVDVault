using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.Director;
public interface IUpdateDirectorNameHandler
{
    Task<IResponse> Handle(UpdateDirectorNameRequest request, CancellationToken cancellationToken);
}
