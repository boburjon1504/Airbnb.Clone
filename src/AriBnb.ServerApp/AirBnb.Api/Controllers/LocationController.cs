using AirBnb.Api.Models.Dtos;
using AirBnb.Application.Services;
using AirBnb.Domain.Entities;
using AirBnb.Persistence.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController(ILocationService locationService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        return Ok(await locationService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id) =>
        Ok(await locationService.GetByIdAsync(id, true, HttpContext.RequestAborted));

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] LocationDto locationDto) =>
        Ok(await locationService.CreateAsync(mapper.Map<Location>(locationDto), true, HttpContext.RequestAborted));

    [HttpPut("{id:guid}")]
    public async ValueTask<IActionResult> Update([FromRoute] Guid id, [FromBody] LocationDto locationDto)
    {
        var foundLocation = await locationService.GetByIdAsync(id, true, HttpContext.RequestAborted);
        var updated = mapper.Map(locationDto, foundLocation);

        return foundLocation is not null ? Ok(await locationService
            .UpdateAsync(updated!, true, HttpContext.RequestAborted)) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> Delete([FromRoute] Guid id) =>
        Ok(await locationService.DeleteByIdAsync(id, true, HttpContext.RequestAborted));
}