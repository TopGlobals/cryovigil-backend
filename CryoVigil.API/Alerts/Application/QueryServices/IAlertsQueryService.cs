using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Queries;

namespace CryoVigil.API.Alerts.Application.QueryServices;

public interface IAlertsQueryService
{
    Task<IEnumerable<Alert>> Handle(GetAllAlertsQuery query, CancellationToken cancellationToken = default);

    Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken = default);
}
