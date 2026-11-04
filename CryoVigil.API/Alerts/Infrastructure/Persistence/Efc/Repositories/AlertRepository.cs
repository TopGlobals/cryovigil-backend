using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Alerts.Infrastructure.Persistence.Efc.Repositories;

public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<IEnumerable<Alert>> FindByLaboratoryIdAsync(int laboratoryId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Where(a => a.LaboratoryId == laboratoryId)
            .ToListAsync(cancellationToken);
    }
}