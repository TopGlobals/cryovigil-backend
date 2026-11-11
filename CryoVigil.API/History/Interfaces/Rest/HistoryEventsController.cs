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
[Tags("Available History endpoints")]
public class HistoryEventsController(
    IHistoryEventCommandService historyEventCommandService,
    IHistoryEventQueryService historyEventQueryService) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a history event")]
    [EndpointDescription("Registers a new incident or system action into the history log.")]
    [ProducesResponseType(typeof(HistoryEventResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHistoryEvent(
        [FromBody] CreateHistoryEventResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateHistoryEventCommandFromResourceAssembler.ToCommandFromResource(resource);
        var historyEvent = await historyEventCommandService.Handle(command, cancellationToken);
        if (historyEvent == null) return BadRequest();

        var historyEventResource = HistoryEventResourceFromEntityAssembler.ToResourceFromEntity(historyEvent);
        return CreatedAtAction(nameof(GetHistoryEventById), new { id = historyEventResource.Id }, historyEventResource);
    }

    [HttpGet]
    [EndpointSummary("Get all history events")]
    [EndpointDescription("Retrieves all logged history events with actions taken and timestamps.")]
    [ProducesResponseType(typeof(IEnumerable<HistoryEventResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllHistoryEvents(CancellationToken cancellationToken)
    {
        var historyEvents = await historyEventQueryService.Handle(new GetAllHistoryEventsQuery(), cancellationToken);
        var resources = historyEvents.Select(HistoryEventResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Get a history event by ID")]
    [EndpointDescription("Retrieves a specific history event by its unique integer identifier.")]
    [ProducesResponseType(typeof(HistoryEventResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHistoryEventById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var historyEvent = await historyEventQueryService.Handle(new GetHistoryEventByIdQuery(id), cancellationToken);
        if (historyEvent == null) return NotFound();

        var resource = HistoryEventResourceFromEntityAssembler.ToResourceFromEntity(historyEvent);
        return Ok(resource);
    }
}
