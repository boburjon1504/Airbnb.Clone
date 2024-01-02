using AirBnb.Api.Models.Dtos;
using AirBnb.Application.Services;
using AirBnb.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationCategoriesController(ILocationCategoriesService locationCategoriesService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAll()
    {
        return Ok(await locationCategoriesService.GetAllAsync());
    }

    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id) =>
        Ok(await locationCategoriesService.GetByIdAsync(id, true, HttpContext.RequestAborted));

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] LocationCategoriesDto locationDto) =>
        Ok(await locationCategoriesService.CreateAsync(mapper.Map<LocationCategories>(locationDto), true, HttpContext.RequestAborted));

    [HttpPut("{id:guid}")]
    public async ValueTask<IActionResult> Update([FromRoute] Guid id, [FromBody] LocationCategoriesDto locationDto)
    {
        var foundLocation = await locationCategoriesService.GetByIdAsync(id, true, HttpContext.RequestAborted);
        var updated = mapper.Map(locationDto, foundLocation);

        return foundLocation is not null ? Ok(await locationCategoriesService
            .UpdateAsync(updated!, true, HttpContext.RequestAborted)) : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> Delete([FromRoute] Guid id) =>
        Ok(await locationCategoriesService.DeleteByIdAsync(id, true, HttpContext.RequestAborted));
}