using CryoVigil.API.Settings.Domain.Model.Aggregates;

namespace CryoVigil.API.Settings.Domain.Repositories;

public interface IGeneralSettingsRepository
{
    Task<GeneralSettings?> FindGlobalAsync(CancellationToken cancellationToken = default);

    Task<GeneralSettings?> FindGlobalAsNoTrackingAsync(CancellationToken cancellationToken = default);

    Task<GeneralSettings?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<GeneralSettings?> FindByIdAsNoTrackingAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<GeneralSettings>> ListAsNoTrackingAsync(CancellationToken cancellationToken = default);

    Task AddAsync(GeneralSettings settings, CancellationToken cancellationToken = default);

    void Remove(GeneralSettings settings);
}
