using DVDVault.Application.Abstractions.DVDs;
using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Application.UseCases.DVDs.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DVDVault.WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DVDController : ControllerBase
{
    private readonly ICreateDVDHandler _createDVDHandler;
    private readonly IUpdateDVDTitleHandler _updateDVDTitleHandler;

    public DVDController(ICreateDVDHandler createDVDHandler, IUpdateDVDTitleHandler updateDVDTitleHandler)
    {
        _createDVDHandler = createDVDHandler;
        _updateDVDTitleHandler = updateDVDTitleHandler;

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedSuccessfully))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
    public async Task<IActionResult> CreateDVD(CreateDVDRequest request, CancellationToken cancellationToken)
    {
        var response = await _createDVDHandler.Handle(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        return Created("/DVD/CreateDVD", response.Message);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedSuccessfully))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InvalidRequest))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(UpdateDVDError))]

    public async Task<IActionResult> UpdateDVDTitle(UpdateDVDTitleRequest request, CancellationToken cancellationToken)
    {
        var response = await _updateDVDTitleHandler.Handle(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return StatusCode(500);

        return Ok(response);
    }
}
