using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Domain.Interfaces.Abstractions;


namespace DVDVault.Application.Abstractions.Director;
public interface IHardDeleteDirectorHandler
{
    Task<IResponse> Handle(HardDeleteDirectorRequest request, CancellationToken cancellationToken);
}
