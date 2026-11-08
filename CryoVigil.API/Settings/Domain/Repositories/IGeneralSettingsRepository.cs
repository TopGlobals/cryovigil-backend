using CryoVigil.API.Settings.Domain.Model.Aggregates;

namespace CryoVigil.API.Settings.Domain.Repositories;

public interface IGeneralSettingsRepository
{
    Task<GeneralSettings?> FindGlobalAsync(CancellationToken cancellationToken = default);

    Task<GeneralSettings?> FindGlobalAsNoTrackingAsync(CancellationToken cancellationToken = default);

    Task AddAsync(GeneralSettings settings, CancellationToken cancellationToken = default);
}
