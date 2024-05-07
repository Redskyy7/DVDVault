﻿using DVDVault.Application.Abstractions.DVDs;
using DVDVault.Application.UseCases.DVDs.Request;
using DVDVault.Application.UseCases.DVDs.Response;
using DVDVault.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DVDVault.WebAPI.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class DVDController : ControllerBase
{
    private readonly ICreateDVDHandler _createDVDHandler;
    private readonly IUpdateDVDTitleHandler _updateDVDTitleHandler;
    private readonly IDVDService _dvdService;

    public DVDController(ICreateDVDHandler createDVDHandler, IUpdateDVDTitleHandler updateDVDTitleHandler, IDVDService dvdService)
    {
        _createDVDHandler = createDVDHandler;
        _updateDVDTitleHandler = updateDVDTitleHandler;
        _dvdService = dvdService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDVD(CreateDVDRequest request, CancellationToken cancellationToken)
    {
        var response = await _createDVDHandler.Handle(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        return Created("/DVD/CreateDVD", response.Message);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDVDTitle(UpdateDVDTitleRequest request, CancellationToken cancellationToken)
    {
        var response = await _updateDVDTitleHandler.Handle(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(response);

        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return StatusCode(500);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var dvds = await _dvdService.GetAllAsync();

        return Ok(dvds);
    }

    [HttpGet]

    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var dvd = await _dvdService.GetByIdAsync(id);

        return Ok(dvd);
    }
}
