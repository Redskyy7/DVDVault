using DVDVault.Application.Abstractions.Director;
using DVDVault.Application.UseCases.Directors.Request;
using DVDVault.Application.UseCases.Directors.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DVDVaultAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DirectorController : ControllerBase
    {

        private readonly ICreateDirectorHandler _createDirectorHandler;

        public DirectorController(ICreateDirectorHandler createDirectorHandler)
        {
           _createDirectorHandler = createDirectorHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedSuccessfully))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
        public Task<IActionResult> CreateDirector(CreateDirectorRequest request, CancellationToken cancellationToken)
        {
            var response = _createDirectorHandler.Handle(request, cancellationToken);

            if (response.Result.StatusCode == HttpStatusCode.BadRequest)
                return Task.FromResult<IActionResult>(BadRequest(response));
            
            return Task.FromResult<IActionResult>(Created("/Director/CreateDirector", response.Result.Message));
        }
    }
}
