using CryoVigil.API.Alerts.Application.QueryServices;
using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Queries;
using CryoVigil.API.Alerts.Domain.Repositories;

namespace CryoVigil.API.Alerts.Application.Internal.QueryServices;

public class AlertsQueryService(IAlertRepository alertRepository) : IAlertsQueryService
{
    public async Task<IEnumerable<Alert>> Handle(GetAlertsByLaboratoryIdQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindByLaboratoryIdAsync(query.LaboratoryId, cancellationToken);
    }
}