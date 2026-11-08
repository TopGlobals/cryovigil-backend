using CryoVigil.API.Settings.Application.QueryServices;
using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Queries;
using CryoVigil.API.Settings.Domain.Repositories;

namespace CryoVigil.API.Settings.Application.Internal.QueryServices;

public class GeneralSettingsQueryService(IGeneralSettingsRepository generalSettingsRepository)
    : IGeneralSettingsQueryService
{
    public async Task<GeneralSettings> Handle(
        GetGeneralSettingsQuery query,
        CancellationToken cancellationToken)
    {
        return await generalSettingsRepository.FindGlobalAsNoTrackingAsync(cancellationToken)
               ?? GeneralSettings.CreateDefault();
    }
}
