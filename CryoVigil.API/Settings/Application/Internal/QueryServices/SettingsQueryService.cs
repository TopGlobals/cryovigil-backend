using CryoVigil.API.Settings.Application.QueryServices;
using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Queries;
using CryoVigil.API.Settings.Domain.Repositories;

namespace CryoVigil.API.Settings.Application.Internal.QueryServices;

public class SettingsQueryService(IGeneralSettingsRepository settingsRepository) : ISettingsQueryService
{
    public async Task<IEnumerable<GeneralSettings>> Handle(
        GetAllSettingsQuery query,
        CancellationToken cancellationToken)
    {
        return await settingsRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    public async Task<GeneralSettings?> Handle(
        GetSettingsByIdQuery query,
        CancellationToken cancellationToken)
    {
        return await settingsRepository.FindByIdAsNoTrackingAsync(query.Id, cancellationToken);
    }
}
