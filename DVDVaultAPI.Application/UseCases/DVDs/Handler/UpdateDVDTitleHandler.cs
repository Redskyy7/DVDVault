using DVDVault.Application.Abstractions.DVDs;
using DVDVault.Application.Abstractions.Response;
using DVDVault.Application.UseCases.Directors.Response;
using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Domain.Interfaces.Abstractions;
using DVDVault.Domain.Interfaces.Repositories;
using DVDVault.Domain.Interfaces.UnitOfWork;
using DVDVault.Domain.Models;
using System.Net;

namespace DVDVault.Application.UseCases.DVDs.Handler;
public class UpdateDVDTitleHandler : IUpdateDVDTitleHandler
{
    private readonly IDVDRepository _dvdRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDVDTitleHandler() { }

    public UpdateDVDTitleHandler(IDVDRepository dvdRepository, IUnitOfWork unitOfWork)
    {
        _dvdRepository = dvdRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(UpdateDVDTitleRequest request, CancellationToken cancellationToken)
    {
        var result = request.Validate();
        if (!result.IsValid)
            return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                        Message: "Invalid request. Please validate the provided data.",
                                        Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

        try
        {
            #region Find Director

            var dvdDB = await _dvdRepository.GetByIdAsync(request.DVDId);
            if (dvdDB is null)
                return new NotFoundDirector(StatusCode: HttpStatusCode.BadRequest,
                                            Message: "Provided director is not registered");
            #endregion

            return await UpdateDVDTitle(request, dvdDB, cancellationToken);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Error while updating director. Details: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> UpdateDVDTitle(UpdateDVDTitleRequest request, DVD dvdDB, CancellationToken cancellationToken)
    {
        dvdDB.UpdateTitle(request.Title.Trim());
        if (!dvdDB.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                            Errors: dvdDB.Errors);

        _unitOfWork.BeginTransaction();

        var updated = await _dvdRepository.UpdateTitleAsync(request.DVDId, dvdDB);
        if (updated == false)
            return new UpdateDirectorError(StatusCode: HttpStatusCode.InternalServerError,
                                        Message: "There was a failure in updating dvd data. Please try again later.");

        await _unitOfWork.Commit(cancellationToken);

        return new UpdatedSuccessfully(StatusCode: HttpStatusCode.OK,
                                    Message: $"{dvdDB.Title} updated succesfully.");
    }
}
