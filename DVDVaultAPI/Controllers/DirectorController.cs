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
        private readonly IUpdateDirectorNameHandler _updateDirectorNameHandler;
        private readonly IUpdateDirectorSurnameHandler _updateDirectorSurnameHandler;
        private readonly ISoftDeleteDirectorHandler _softDeleteDirectorHandler;
        private readonly IHardDeleteDirectorHandler _hardDeleteDirectorHandler;

        public DirectorController(ICreateDirectorHandler createDirectorHandler, IUpdateDirectorNameHandler updateDirectorNameHandler, IUpdateDirectorSurnameHandler updateDirectorSurnameHandler, ISoftDeleteDirectorHandler softDeleteDirectorHandler, IHardDeleteDirectorHandler hardDeleteDirectorHandler)
        {
           _createDirectorHandler = createDirectorHandler;
           _updateDirectorNameHandler = updateDirectorNameHandler;
           _updateDirectorSurnameHandler = updateDirectorSurnameHandler;
           _softDeleteDirectorHandler = softDeleteDirectorHandler;
           _hardDeleteDirectorHandler = hardDeleteDirectorHandler;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedSuccessfully))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
        public async Task<IActionResult> CreateDirector(CreateDirectorRequest request, CancellationToken cancellationToken)
        {
            var response = await _createDirectorHandler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            
            return Created("/Director/CreateDirector", response.Message);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedSuccessfully))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UpdateDirectorError))]

        public async Task<IActionResult> UpdateDirectorName(UpdateDirectorNameRequest request, CancellationToken cancellationToken)
        {
            var response = await _updateDirectorNameHandler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedSuccessfully))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UpdateDirectorError))]
        public async Task<IActionResult> UpdateDirectorSurname(UpdateDirectorSurnameRequest request, CancellationToken cancellationToken)
        {
            var response = await _updateDirectorSurnameHandler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedSuccessfully))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DeleteDirectorError))]
        public async Task<IActionResult> SoftDeleteDirector(SoftDeleteDirectorRequest request, CancellationToken cancellationToken)
        {
            var response = await _softDeleteDirectorHandler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500);

            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedSuccessfully))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DeleteDirectorError))]
        public async Task<IActionResult> HardDeleteDirector(HardDeleteDirectorRequest request, CancellationToken cancellationToken)
        {
            var response = await _hardDeleteDirectorHandler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(response);
            if (response.StatusCode == HttpStatusCode.InternalServerError)
                return StatusCode(500);

            return Ok(response);
        }

    }
}
