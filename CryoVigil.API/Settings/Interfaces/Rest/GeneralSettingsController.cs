using CryoVigil.API.Settings.Application.CommandServices;
using CryoVigil.API.Settings.Application.QueryServices;
using CryoVigil.API.Settings.Domain.Model.Commands;
using CryoVigil.API.Settings.Domain.Model.Queries;
using CryoVigil.API.Settings.Domain.Model.ValueObjects;
using CryoVigil.API.Settings.Interfaces.Rest.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.Settings.Interfaces.Rest;

[ApiController]
[Route("api/v1/settings/general")]
public class GeneralSettingsController(
    IGeneralSettingsQueryService generalSettingsQueryService,
    IGeneralSettingsCommandService generalSettingsCommandService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<GeneralSettingsResource>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GeneralSettingsResource>> Get(CancellationToken cancellationToken)
    {
        var settings = await generalSettingsQueryService.Handle(
            new GetGeneralSettingsQuery(),
            cancellationToken);

        return Ok(new GeneralSettingsResource(settings.Region, settings.TemperatureUnit));
    }

    [HttpPut]
    [ProducesResponseType<GeneralSettingsResource>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GeneralSettingsResource>> Update(
        UpdateGeneralSettingsResource resource,
        CancellationToken cancellationToken)
    {
        var settings = await generalSettingsCommandService.Handle(
            new UpdateGeneralSettingsCommand(
                resource.Region,
                Enum.Parse<TemperatureUnit>(resource.TemperatureUnit)),
            cancellationToken);

        return Ok(new GeneralSettingsResource(settings.Region, settings.TemperatureUnit));
    }
}
