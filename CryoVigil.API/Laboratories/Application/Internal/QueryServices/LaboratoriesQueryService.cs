using CryoVigil.API.Laboratories.Application.QueryServices;
using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Laboratories.Domain.Model.Queries;
using CryoVigil.API.Laboratories.Domain.Repositories;

namespace CryoVigil.API.Laboratories.Application.Internal.QueryServices;

public class LaboratoriesQueryService(ILaboratoryRepository laboratoryRepository)
    : ILaboratoriesQueryService
{
    public async Task<IEnumerable<Laboratory>> Handle(
        GetAllLaboratoriesQuery query,
        CancellationToken cancellationToken)
    {
        return await laboratoryRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    public async Task<Laboratory?> Handle(
        GetLaboratoryByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await laboratoryRepository.FindByIdAsNoTrackingAsync(query.Id, cancellationToken);
    }
}
