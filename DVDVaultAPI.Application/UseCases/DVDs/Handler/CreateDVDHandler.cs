using DVDVault.Application.Abstractions.DVDs;
using DVDVault.Application.Abstractions.Response;
using DVDVault.Application.UseCases.DVDs.Response;
using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;
using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Interfaces.UnitOfWork;
using DVDVault.Domain.Models;
using System.Net;

namespace DVDVault.Application.UseCases.DVDs.Handler;
public class CreateDVDHandler : ICreateDVDHandler
{
    private readonly IDVDRepository _dvdRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDVDHandler(IDVDRepository dvdRepository, IUnitOfWork unitOfWork)
    {
        _dvdRepository = dvdRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(CreateDVDRequest request, CancellationToken cancellationToken)
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
            return await AddDVDAsync(request, cancellationToken);
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

    private async Task<IResponse> AddDVDAsync(CreateDVDRequest request, CancellationToken cancellationToken)
    {
        DVD dvd= new DVD(title: request.Title!.Trim(),
                        genre: request.Genre!,
                        published: request.Published!,
                        copies: request.Copies!,
                        directorId: request.DirectorId!
                        );

        if (!dvd.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                    Errors: dvd.Errors);

        _unitOfWork.BeginTransaction();

        await _dvdRepository.AddAsync(dvd);

        await _unitOfWork.Commit(cancellationToken);

        return new CreatedSuccessfully(StatusCode: HttpStatusCode.Created, Message: "Director created successfully");
    }
}
