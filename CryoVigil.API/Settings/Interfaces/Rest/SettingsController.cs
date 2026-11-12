using CryoVigil.API.Settings.Application.CommandServices;
using CryoVigil.API.Settings.Application.QueryServices;
using CryoVigil.API.Settings.Domain.Model.Commands;
using CryoVigil.API.Settings.Domain.Model.Queries;
using CryoVigil.API.Settings.Domain.Model.ValueObjects;
using CryoVigil.API.Settings.Interfaces.Rest.Resources;
using CryoVigil.API.Settings.Interfaces.Rest.Transform;
using CryoVigil.API.Shared.Domain.Model.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.Settings.Interfaces.Rest;

[ApiController]
[Route("api/v1/settings")]
public class SettingsController(
    ISettingsQueryService settingsQueryService,
    ISettingsCommandService settingsCommandService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<SettingsResource>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SettingsResource>> Create(
        SettingsRequestResource resource,
        CancellationToken cancellationToken)
    {
        var result = await settingsCommandService.Handle(
            new CreateSettingsCommand(ToDetails(resource)),
            cancellationToken);

        if (result.IsFailure)
            return new ActionResult<SettingsResource>(HandleCommandFailure(result.DomainError));

        var createdResource = SettingsResourceFromEntityAssembler.ToResource(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = createdResource.Id }, createdResource);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<SettingsResource>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SettingsResource>> Update(
        int id,
        SettingsRequestResource resource,
        CancellationToken cancellationToken)
    {
        if (resource.Id.HasValue && resource.Id.Value != id)
            ModelState.AddModelError(nameof(resource.Id), "The resource ID must match the route ID.");

        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await settingsCommandService.Handle(
            new UpdateSettingsCommand(id, ToDetails(resource)),
            cancellationToken);

        if (result.IsFailure)
            return new ActionResult<SettingsResource>(HandleCommandFailure(result.DomainError));

        return Ok(SettingsResourceFromEntityAssembler.ToResource(result.Value!));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await settingsCommandService.Handle(
            new DeleteSettingsCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return HandleCommandFailure(result.DomainError);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<SettingsResource>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<SettingsResource>>> GetAll(CancellationToken cancellationToken)
    {
        var settings = await settingsQueryService.Handle(
            new GetAllSettingsQuery(),
            cancellationToken);

        return Ok(settings.Select(SettingsResourceFromEntityAssembler.ToResource));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<SettingsResource>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SettingsResource>> GetById(int id, CancellationToken cancellationToken)
    {
        var settings = await settingsQueryService.Handle(
            new GetSettingsByIdQuery(id),
            cancellationToken);

        if (settings is null)
            return NotFound();

        return Ok(SettingsResourceFromEntityAssembler.ToResource(settings));
    }

    private static SettingsDetails ToDetails(SettingsRequestResource resource)
    {
        var tempUnit = Enum.TryParse<TemperatureUnit>(resource.General?.TemperatureUnit, ignoreCase: true, out var parsedUnit)
            ? parsedUnit
            : TemperatureUnit.Celsius;

        var notifications = resource.Notifications is not null
            ? new NotificationSettingsDetails(
                resource.Notifications.Email,
                resource.Notifications.InApp,
                resource.Notifications.DailySummary,
                resource.Notifications.WeeklyReport,
                resource.Notifications.InstantAlerts,
                resource.Notifications.WarningThreshold,
                resource.Notifications.CriticalThreshold)
            : new NotificationSettingsDetails(true, false, false, false, true, "2", "5");

        var sensors = (resource.Sensors ?? [])
            .Where(sensor => sensor is not null)
            .Select(sensor => new MonitoringSensorDetails(
                sensor.Id,
                sensor.Name,
                sensor.Model,
                sensor.Serial,
                sensor.Type,
                sensor.Location,
                sensor.Status))
            .ToList();

        return new SettingsDetails(
            resource.General?.Region ?? "Europe/Zurich",
            tempUnit,
            notifications,
            resource.Security?.TwoFactorEnabled ?? false,
            sensors);
    }

    private ObjectResult HandleCommandFailure(Error error)
    {
        var statusCode = error.Type switch
        {
            EErrorType.Conflict => StatusCodes.Status409Conflict,
            EErrorType.NotFound => StatusCodes.Status404NotFound,
            EErrorType.Validation => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status400BadRequest
        };

        var title = error.Type switch
        {
            EErrorType.Conflict => "Settings conflict",
            EErrorType.NotFound => "Settings not found",
            _ => "Bad request"
        };

        return new ObjectResult(new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = error.Message,
            Instance = HttpContext.Request.Path
        })
        {
            StatusCode = statusCode
        };
    }
}
