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
public class SoftDeleteDVDHandler : ISoftDeleteDVDHandler
{
    private readonly IDVDRepository _dvdRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteDVDHandler() { }

    public SoftDeleteDVDHandler(IDVDRepository dvdRepository, IUnitOfWork unitOfWork)
    {
        _dvdRepository = dvdRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(SoftDeleteDVDRequest request, CancellationToken cancellationToken)
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

            return await SoftDeleteDVD(dvdDB, cancellationToken);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Error while deleting DVD. Details: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> SoftDeleteDVD(DVD dvdDB, CancellationToken cancellationToken)
    {
        dvdDB.SoftDelete();
        if (!dvdDB.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                            Errors: dvdDB.Errors);

        _unitOfWork.BeginTransaction();

        var deleted = await _dvdRepository.SoftDelete(dvdDB.Id, dvdDB);
        if (deleted == false)
            return new DeleteDVDError(StatusCode: HttpStatusCode.InternalServerError,
                                      Message: "There was a failure in DVD deletion. Please try again later.");

        await _unitOfWork.Commit(cancellationToken);

        return new DeletedSuccessfully(StatusCode: HttpStatusCode.OK,
                                    Message: $"{dvdDB.Title} deleted successfully.");
    }
}
