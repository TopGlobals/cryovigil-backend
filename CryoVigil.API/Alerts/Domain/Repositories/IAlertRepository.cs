using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Shared.Domain.Repositories;

namespace CryoVigil.API.Alerts.Domain.Repositories;

public interface IAlertRepository : IBaseRepository<Alert>
{
    Task<IEnumerable<Alert>> FindByLaboratoryIdAsync(int laboratoryId, CancellationToken cancellationToken = default);
}