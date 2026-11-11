using System.Net.Mime;
using CryoVigil.API.History.Application.CommandServices;
using CryoVigil.API.History.Application.QueryServices;
using CryoVigil.API.History.Domain.Model.Queries;
using CryoVigil.API.History.Interfaces.Rest.Resources;
using CryoVigil.API.History.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.History.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class HistoryEventsController : ControllerBase
{
    private readonly IHistoryEventCommandService _historyEventCommandService;
    private readonly IHistoryEventQueryService _historyEventQueryService;

    public HistoryEventsController(
        IHistoryEventCommandService historyEventCommandService,
        IHistoryEventQueryService historyEventQueryService)
    {
        _historyEventCommandService = historyEventCommandService;
        _historyEventQueryService = historyEventQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHistoryEvent([FromBody] CreateHistoryEventResource resource)
    {
        var createHistoryEventCommand = CreateHistoryEventCommandFromResourceAssembler.ToCommandFromResource(resource);
        var historyEvent = await _historyEventCommandService.Handle(createHistoryEventCommand);
        if (historyEvent == null) return BadRequest();
        var historyEventResource = HistoryEventResourceFromEntityAssembler.ToResourceFromEntity(historyEvent);
        return CreatedAtAction(nameof(GetHistoryEventById), new { id = historyEventResource.Id }, historyEventResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllHistoryEvents()
    {
        var getAllHistoryEventsQuery = new GetAllHistoryEventsQuery();
        var historyEvents = await _historyEventQueryService.Handle(getAllHistoryEventsQuery);
        var resources = historyEvents.Select(HistoryEventResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetHistoryEventById(int id)
    {
        var getHistoryEventByIdQuery = new GetHistoryEventByIdQuery(id);
        var historyEvent = await _historyEventQueryService.Handle(getHistoryEventByIdQuery);
        if (historyEvent == null) return NotFound();
        var resource = HistoryEventResourceFromEntityAssembler.ToResourceFromEntity(historyEvent);
        return Ok(resource);
    }
}