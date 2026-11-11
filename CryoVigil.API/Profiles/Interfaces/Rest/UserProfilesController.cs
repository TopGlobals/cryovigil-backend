using CryoVigil.API.Profiles.Application.CommandServices;
using CryoVigil.API.Profiles.Application.QueryServices;
using CryoVigil.API.Profiles.Domain.Model.Commands;
using CryoVigil.API.Profiles.Domain.Model.Queries;
using CryoVigil.API.Profiles.Interfaces.Rest.Resources;
using CryoVigil.API.Profiles.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.Profiles.Interfaces.Rest;

[ApiController]
[Route("api/v1/profiles/current")]
public class UserProfilesController(
    IUserProfileQueryService profileQueryService,
    IUserProfileCommandService profileCommandService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<UserProfileResource>(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserProfileResource>> Get(CancellationToken cancellationToken)
    {
        var profile = await profileQueryService.Handle(
            new GetCurrentProfileQuery(),
            cancellationToken);

        return Ok(UserProfileResourceFromEntityAssembler.ToResource(profile));
    }

    [HttpPut]
    [ProducesResponseType<UserProfileResource>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserProfileResource>> Update(
        UserProfileResource resource,
        CancellationToken cancellationToken)
    {
        var profile = await profileCommandService.Handle(
            new UpdateCurrentProfileCommand(resource.Name, resource.Email, resource.Role),
            cancellationToken);

        return Ok(UserProfileResourceFromEntityAssembler.ToResource(profile));
    }
}
