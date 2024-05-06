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
public class UpdateDirectorSurnameHandler : IUpdateDirectorSurnameHandler
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDirectorSurnameHandler() { }

    public UpdateDirectorSurnameHandler(IDirectorRepository directorRepository, IUnitOfWork unitOfWork)
    {
        _directorRepository = directorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(UpdateDirectorSurnameRequest request, CancellationToken cancellationToken)
    {
        var result = request.Validate();
        if (!result.IsValid)
            return new InvalidRequest(StatusCode: HttpStatusCode.BadRequest,
                                        Message: "Invalid request. Please validate the provided data.",
                                        Errors: result.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage));

        try
        {
            #region Find Director

            var directorDB = await _directorRepository.GetByIdAsync(request.DirectorId);
            if (directorDB is null)
                return new NotFoundDirector(StatusCode: HttpStatusCode.BadRequest,
                                            Message: "Provided director is not registered");
            #endregion

            return await UpdateDirectorSurname(request, directorDB, cancellationToken);
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

    private async Task<IResponse> UpdateDirectorSurname(UpdateDirectorSurnameRequest request, Director directorDB, CancellationToken cancellationToken)
    {
        directorDB.UpdateSurname(request.Surname.Trim());
        if (!directorDB.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                            Errors: directorDB.Errors);

        _unitOfWork.BeginTransaction();

        var updated = await _directorRepository.UpdateSurnameAsync(request.DirectorId, directorDB);
        if (updated == false)
            return new UpdateDirectorError(StatusCode: HttpStatusCode.InternalServerError,
                                        Message: "There was a failure in updating director data. Please try again later.");

        await _unitOfWork.Commit(cancellationToken);

        return new UpdatedSuccessfully(StatusCode: HttpStatusCode.OK,
                                    Message: $"{directorDB.Surname} updated succesfully.");
    }
}
