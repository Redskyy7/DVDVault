using DVDVault.Application.Abstractions.DVDs;
using DVDVault.Application.Abstractions.Response;
using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Application.UseCases.DVDs.Response;
using DVDVault.Domain.Interfaces.Abstractions;
using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Interfaces.UnitOfWork;
using DVDVault.Domain.Models;
using System.Net;

namespace DVDVault.Application.UseCases.DVDs.Handler;
public class RentCopyHandler : IRentCopyHandler
{
    private readonly IDVDRepository _dvdRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RentCopyHandler()
    {
        
    }

    public RentCopyHandler(IDVDRepository dvdRepository, IUnitOfWork unitOfWork)
    {
        _dvdRepository = dvdRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(RentCopyDVDRequest request, CancellationToken cancellationToken)
    {
        var result = request.Validate();
        if (!result.IsValid)
            return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                        Message: "Invalid request. Please validate the provided data.",
                                        Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

        try
        {
            #region Find DVD

            var dvdDB = await _dvdRepository.GetByIdAsync(request.DVDId);
            if (dvdDB is null)
                return new NotFoundDVD(StatusCode: HttpStatusCode.BadRequest,
                                            Message: "Provided DVD is not registered");
            #endregion

            if (dvdDB.Copies <= 0)
                return new UpdateDVDError(StatusCode: HttpStatusCode.InternalServerError,
                    Message: $"There are no copies available for this DVD. Copies:{dvdDB.Copies}");

            return await RentCopy(request, dvdDB, cancellationToken);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Error while returning DVD. Details: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> RentCopy(RentCopyDVDRequest request, DVD dvdDB, CancellationToken cancellationToken)
    {
        dvdDB.RentCopy();
        if (!dvdDB.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                            Errors: dvdDB.Errors);

        _unitOfWork.BeginTransaction();

        var returned = await _dvdRepository.UpdateCopiesAsync(request.DVDId, dvdDB);
        if (returned == false)
            return new UpdateDVDError(StatusCode: HttpStatusCode.InternalServerError,
                                        Message: "There was a failure in renting the DVD. Please try again later.");

        await _unitOfWork.Commit(cancellationToken);

        return new UpdatedSuccessfully(StatusCode: HttpStatusCode.OK,
            Message: $"{dvdDB.Title} rented successfully");
    }
}
