using DVDVault.Application.Abstractions.Director;
using DVDVault.Application.Abstractions.Response;
using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Application.UseCases.Directors.Response;
using DVDVault.Domain.Interfaces.Abstractions;
using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Interfaces.UnitOfWork;
using DVDVault.Domain.Models;
using System.Net;

namespace DVDVault.Application.UseCases.Directors.Handler;


public class CreateDirectorHandler : ICreateDirectorHandler
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDirectorHandler(IDirectorRepository directorRepository, IUnitOfWork unitOfWork)
    {
        _directorRepository = directorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(CreateDirectorRequest request, CancellationToken cancellationToken)
    {
        var result = request.Validate();

        if (!result.IsValid)
        {
            return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                        Message: "Invalid request. Please validate the provided data.",
                                        Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));
        }
        try
        {
            return await AddDirectorAsync(request, cancellationToken);
        }
        catch (Exception)
        {
            _unitOfWork.Rollback();
            throw;
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> AddDirectorAsync(CreateDirectorRequest request, CancellationToken cancellationToken)
    {
        Director director = new Director(name: request.Name!.Trim(),
                                         surname: request.Surname!.Trim()
                                         );

        if (!director.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                    Errors: director.Errors);

        _unitOfWork.BeginTransaction();

        await _directorRepository.AddAsync(director);

        await _unitOfWork.Commit(cancellationToken);

        return new CreatedSuccessfully(StatusCode: HttpStatusCode.Created, Message: "Director created successfully");
    }
}
