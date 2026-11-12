using System.Net.Mime;
using CryoVigil.API.OperationEvents.Application.CommandServices;
using CryoVigil.API.OperationEvents.Application.QueryServices;
using CryoVigil.API.OperationEvents.Domain.Model.Commands;
using CryoVigil.API.OperationEvents.Domain.Model.Queries;
using CryoVigil.API.OperationEvents.Interfaces.Rest.Resources;
using CryoVigil.API.OperationEvents.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.OperationEvents.Interfaces.Rest;

[ApiController]
[Route("api/v1/history")]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Operation Events endpoints")]
public class HistoryEventsController(
    IHistoryEventCommandService historyEventCommandService,
    IHistoryEventQueryService historyEventQueryService) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create an Operation event")]
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
    [EndpointSummary("Get all Operation events")]
    [EndpointDescription("Retrieves all logged history events with actions taken and timestamps.")]
    [ProducesResponseType(typeof(IEnumerable<HistoryEventResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllHistoryEvents(CancellationToken cancellationToken)
    {
        var historyEvents = await historyEventQueryService.Handle(new GetAllHistoryEventsQuery(), cancellationToken);
        var resources = historyEvents.Select(HistoryEventResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Get an Operation event by ID")]
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

    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete an Operation event")]
    [EndpointDescription("Deletes a specific history event from the log by its unique integer identifier.")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHistoryEvent([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteHistoryEventCommand(id);
        var result = await historyEventCommandService.Handle(command, cancellationToken);
        if (!result) return NotFound();

        return NoContent();
    }
}