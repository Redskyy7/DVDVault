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
public class SoftDeleteDirectorHandler : ISoftDeleteDirectorHandler
{
    private readonly IDirectorRepository _directorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteDirectorHandler() { }

    public SoftDeleteDirectorHandler(IDirectorRepository directorRepository, IUnitOfWork unitOfWork)
    {
        _directorRepository = directorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse> Handle(SoftDeleteDirectorRequest request, CancellationToken cancellationToken)
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

            return await SoftDeleteDirector(directorDB, cancellationToken);
        }
        catch (Exception ex)
        {
            _unitOfWork.Rollback();
            throw new Exception($"Error while deleting director. Details: {ex.Message}");
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    private async Task<IResponse> SoftDeleteDirector(Director directorDB, CancellationToken cancellationToken)
    {
        directorDB.SoftDelete();
        if (!directorDB.IsValid)
            return new DomainNotification(StatusCode: HttpStatusCode.BadRequest,
                                            Errors: directorDB.Errors);
        
        _unitOfWork.BeginTransaction();

        var deleted = await _directorRepository.SoftDelete(directorDB.Id, directorDB);
        if (deleted == false)
            return new DeleteDirectorError(StatusCode: HttpStatusCode.InternalServerError,
                                     Message: "There was a failure in director deletion. Please try again later.");

        await _unitOfWork.Commit(cancellationToken);

        return new DeletedSuccessfully(StatusCode: HttpStatusCode.OK,
                                     Message: $"{directorDB.Name} deleted successfully.");
    }
}
