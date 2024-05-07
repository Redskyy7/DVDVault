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

    public DVDController(ICreateDVDHandler createDVDHandler)
    {
        _createDVDHandler = createDVDHandler;
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
}
