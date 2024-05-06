using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Domain.Interfaces.Abstractions;

namespace DVDVault.Application.Abstractions.Director;
public interface IUpdateDirectorSurnameHandler
{
    Task<IResponse> Handle(UpdateDirectorSurnameRequest request, CancellationToken cancellationToken);
}
