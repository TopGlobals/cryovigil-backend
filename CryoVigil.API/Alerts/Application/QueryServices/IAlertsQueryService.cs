using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Queries;

namespace CryoVigil.API.Alerts.Application.QueryServices;

public interface IAlertsQueryService
{
    Task<IEnumerable<Alert>> Handle(GetAlertsByLaboratoryIdQuery query, CancellationToken cancellationToken);
}