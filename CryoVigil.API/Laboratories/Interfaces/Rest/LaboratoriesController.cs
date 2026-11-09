using CryoVigil.API.Laboratories.Application.CommandServices;
using CryoVigil.API.Laboratories.Application.QueryServices;
using CryoVigil.API.Laboratories.Domain.Model.Commands;
using CryoVigil.API.Laboratories.Domain.Model.Queries;
using CryoVigil.API.Laboratories.Domain.Model.ValueObjects;
using CryoVigil.API.Laboratories.Interfaces.Rest.Resources;
using CryoVigil.API.Laboratories.Interfaces.Rest.Transform;
using CryoVigil.API.Shared.Domain.Model.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.Laboratories.Interfaces.Rest;

[ApiController]
[Route("api/v1/laboratories")]
public class LaboratoriesController(
    ILaboratoriesQueryService laboratoriesQueryService,
    ILaboratoriesCommandService laboratoriesCommandService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<LaboratoryResource>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<LaboratoryResource>> Create(
        CreateLaboratoryResource resource,
        CancellationToken cancellationToken)
    {
        var result = await laboratoriesCommandService.Handle(
            new CreateLaboratoryCommand(ToDetails(resource)),
            cancellationToken);

        if (result.IsFailure)
        {
            return HandleCommandFailure(result.DomainError);
        }

        var createdResource = LaboratoryResourceFromEntityAssembler.ToResource(result.Value!);
        return CreatedAtAction(nameof(GetById), new { id = createdResource.Id }, createdResource);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType<LaboratoryResource>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<LaboratoryResource>> Update(
        int id,
        CreateLaboratoryResource resource,
        CancellationToken cancellationToken)
    {
        var result = await laboratoriesCommandService.Handle(
            new UpdateLaboratoryCommand(id, ToDetails(resource)),
            cancellationToken);

        if (result.IsFailure)
            return HandleCommandFailure(result.DomainError);

        return Ok(LaboratoryResourceFromEntityAssembler.ToResource(result.Value!));
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<LaboratoryResource>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LaboratoryResource>>> GetAll(CancellationToken cancellationToken)
    {
        var laboratories = await laboratoriesQueryService.Handle(
            new GetAllLaboratoriesQuery(),
            cancellationToken);

        return Ok(laboratories.Select(LaboratoryResourceFromEntityAssembler.ToResource));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType<LaboratoryResource>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LaboratoryResource>> GetById(int id, CancellationToken cancellationToken)
    {
        var laboratory = await laboratoriesQueryService.Handle(
            new GetLaboratoryByIdQuery(id),
            cancellationToken);

        if (laboratory is null)
            return NotFound();

        return Ok(LaboratoryResourceFromEntityAssembler.ToResource(laboratory));
    }

    private static LaboratoryDetails ToDetails(CreateLaboratoryResource resource)
    {
        return new LaboratoryDetails(
            resource.Name,
            resource.Code,
            Enum.Parse<LaboratoryType>(resource.Type, ignoreCase: true),
            resource.Building,
            resource.Floor,
            resource.Room,
            resource.Description,
            resource.TemperatureMin,
            resource.TemperatureMax,
            resource.Co2Max,
            Enum.Parse<GasSensitivity>(resource.GasSensitivity, ignoreCase: true),
            resource.VibrationMax,
            Enum.Parse<LaboratoryEscalation>(resource.Escalation, ignoreCase: true),
            resource.Sensors,
            resource.Notifications);
    }

    private ActionResult<LaboratoryResource> HandleCommandFailure(Error error)
    {
        var statusCode = error.Type switch
        {
            EErrorType.Conflict => StatusCodes.Status409Conflict,
            EErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => throw new InvalidOperationException($"Unexpected laboratory command error: {error.Code}")
        };

        return new ObjectResult(new ProblemDetails
        {
            Status = statusCode,
            Title = error.Type == EErrorType.Conflict
                ? "Laboratory code already exists"
                : "Laboratory not found",
            Detail = error.Message,
            Instance = HttpContext.Request.Path
        })
        {
            StatusCode = statusCode
        };
    }
}
