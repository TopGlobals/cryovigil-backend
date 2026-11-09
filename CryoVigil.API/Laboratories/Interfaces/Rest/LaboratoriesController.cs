using CryoVigil.API.Laboratories.Application.QueryServices;
using CryoVigil.API.Laboratories.Domain.Model.Queries;
using CryoVigil.API.Laboratories.Interfaces.Rest.Resources;
using CryoVigil.API.Laboratories.Interfaces.Rest.Transform;
using Microsoft.AspNetCore.Mvc;

namespace CryoVigil.API.Laboratories.Interfaces.Rest;

[ApiController]
[Route("api/v1/laboratories")]
public class LaboratoriesController(ILaboratoriesQueryService laboratoriesQueryService) : ControllerBase
{
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
}
