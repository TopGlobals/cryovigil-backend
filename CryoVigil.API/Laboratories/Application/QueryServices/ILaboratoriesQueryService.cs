using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using CryoVigil.API.Laboratories.Domain.Model.Queries;

namespace CryoVigil.API.Laboratories.Application.QueryServices;

public interface ILaboratoriesQueryService
{
    Task<IEnumerable<Laboratory>> Handle(GetAllLaboratoriesQuery query, CancellationToken cancellationToken);

    Task<Laboratory?> Handle(GetLaboratoryByIdQuery query, CancellationToken cancellationToken);
}
