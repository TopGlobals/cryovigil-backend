using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Alerts.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<Alert?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Include(a => a.CorrectiveActions)
            .Include(a => a.Readings)
            .FirstOrDefaultAsync(a => a.Id == new AlertId(id), cancellationToken);
    }

    public async Task<IEnumerable<Alert>> ListWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Include(a => a.CorrectiveActions)
            .Include(a => a.Readings)
            .OrderByDescending(a => a.RaisedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Alert?> FindByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Include(a => a.CorrectiveActions)
            .Include(a => a.Readings)
            .FirstOrDefaultAsync(a => a.Code == code, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .AnyAsync(a => a.Code == code, cancellationToken);
    }
}
