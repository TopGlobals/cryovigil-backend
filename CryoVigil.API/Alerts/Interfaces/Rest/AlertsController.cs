using System.Net.Mime;
using CryoVigil.API.Alerts.Application.CommandServices;
using CryoVigil.API.Alerts.Application.QueryServices;
using CryoVigil.API.Alerts.Domain.Model.Queries;
using CryoVigil.API.Alerts.Interfaces.Rest.Resources;
using CryoVigil.API.Alerts.Interfaces.Rest.Transform;
using CryoVigil.API.Shared.Interfaces.Rest.ProblemDetails;
using CryoVigil.API.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace CryoVigil.API.Alerts.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Available Alert endpoints")]
public class AlertsController(
    IAlertsQueryService alertsQueryService,
    IAlertsCommandService alertsCommandService,
    IStringLocalizer<ErrorMessages> errorLocalizer,
    ProblemDetailsFactory problemDetailsFactory) : ControllerBase
{
    [HttpGet]
    [EndpointSummary("Get all alerts")]
    [EndpointDescription("Retrieves all alerts with their associated corrective actions and sensor readings.")]
    [ProducesResponseType(typeof(IEnumerable<AlertResource>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAlerts(CancellationToken cancellationToken)
    {
        var alerts = await alertsQueryService.Handle(new GetAllAlertsQuery(), cancellationToken);
        var resources = alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Get an alert by ID")]
    [EndpointDescription("Retrieves a specific alert by its unique integer identifier.")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlertById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var alert = await alertsQueryService.Handle(new GetAlertByIdQuery(id), cancellationToken);

        return AlertActionResultAssembler.ToActionResultFromGetAlertByIdResult(
            this,
            alert,
            errorLocalizer,
            problemDetailsFactory,
            found => Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(found)));
    }

    [HttpPost]
    [EndpointSummary("Create an alert")]
    [EndpointDescription("Creates a new alert (e.g. reported manually by laboratory staff).")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAlert(
        [FromBody] CreateAlertResource resource,
        CancellationToken cancellationToken)
    {
        var command = CreateAlertCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await alertsCommandService.Handle(command, cancellationToken);

        return AlertActionResultAssembler.ToActionResultFromCreateAlertResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            created => CreatedAtAction(
                nameof(GetAlertById),
                new { id = created.Id.Value },
                AlertResourceFromEntityAssembler.ToResourceFromEntity(created)));
    }

    [HttpPut("{id:int}")]
    [EndpointSummary("Update an alert")]
    [EndpointDescription("Updates an existing alert (e.g. acknowledging or resolving an incident with corrective action).")]
    [ProducesResponseType(typeof(AlertResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAlert(
        [FromRoute] int id,
        [FromBody] UpdateAlertResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateAlertCommandFromResourceAssembler.ToCommandFromResource(id, resource);
        var result = await alertsCommandService.Handle(command, cancellationToken);

        return AlertActionResultAssembler.ToActionResultFromUpdateAlertResult(
            this,
            result,
            errorLocalizer,
            problemDetailsFactory,
            updated => Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(updated)));
    }
}
