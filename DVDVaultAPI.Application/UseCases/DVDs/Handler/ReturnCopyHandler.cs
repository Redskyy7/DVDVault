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
public class ReturnCopyHandler : IReturnCopyHandler
{
    private readonly IDVDRepository _dvdRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReturnCopyHandler()
    {
        
    }

    public ReturnCopyHandler(IDVDRepository dvdRepository, IUnitOfWork unitOfWork)
    {
        _dvdRepository = dvdRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(ReturnCopyDVDRequest request, CancellationToken cancellationToken)
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

            return await ReturnCopy(request, dvdDB, cancellationToken);
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

    private async Task<IResponse> ReturnCopy(ReturnCopyDVDRequest request, DVD dvdDB, CancellationToken cancellationToken)
    {
        dvdDB.ReturnCopy();
        if (!dvdDB.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                            Errors: dvdDB.Errors);

        _unitOfWork.BeginTransaction();

        var returned = await _dvdRepository.UpdateCopiesAsync(request.DVDId, dvdDB);
        if (returned == false)
            return new UpdateDVDError(StatusCode: HttpStatusCode.InternalServerError,
                                        Message: "There was a failure in updating DVD data. Please try again later.");

        await _unitOfWork.Commit(cancellationToken);

        return new UpdatedSuccessfully(StatusCode: HttpStatusCode.OK,
            Message: $"{dvdDB.Title} returned successfully");
    }
}
