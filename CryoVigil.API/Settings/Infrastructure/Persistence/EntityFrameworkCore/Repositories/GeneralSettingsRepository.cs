using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Settings.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class GeneralSettingsRepository(AppDbContext context) : IGeneralSettingsRepository
{
    public async Task<GeneralSettings?> FindGlobalAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<GeneralSettings>()
            .FindAsync(new object[] { GeneralSettings.GlobalSettingsId }, cancellationToken);
    }

    public async Task<GeneralSettings?> FindGlobalAsNoTrackingAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<GeneralSettings>()
            .AsNoTracking()
            .SingleOrDefaultAsync(settings => settings.Id == GeneralSettings.GlobalSettingsId, cancellationToken);
    }

    public async Task<GeneralSettings?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await context.Set<GeneralSettings>()
            .FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<GeneralSettings?> FindByIdAsNoTrackingAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await context.Set<GeneralSettings>()
            .AsNoTracking()
            .SingleOrDefaultAsync(settings => settings.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<GeneralSettings>> ListAsNoTrackingAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Set<GeneralSettings>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(GeneralSettings settings, CancellationToken cancellationToken = default)
    {
        await context.Set<GeneralSettings>().AddAsync(settings, cancellationToken);
    }

    public void Remove(GeneralSettings settings)
    {
        context.Set<GeneralSettings>().Remove(settings);
    }
}
