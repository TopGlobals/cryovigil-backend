using CryoVigil.API.Alerts.Application.QueryServices;
using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Queries;
using CryoVigil.API.Alerts.Domain.Repositories;

namespace CryoVigil.API.Alerts.Application.Internal.QueryServices;

public class AlertsQueryService(IAlertRepository alertRepository) : IAlertsQueryService
{
    public async Task<IEnumerable<Alert>> Handle(
        GetAllAlertsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await alertRepository.ListWithDetailsAsync(cancellationToken);
    }

    public async Task<Alert?> Handle(
        GetAlertByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        return await alertRepository.FindByIdWithDetailsAsync(query.AlertId, cancellationToken);
    }
}
