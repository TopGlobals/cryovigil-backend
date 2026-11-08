using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Queries;

namespace CryoVigil.API.Settings.Application.QueryServices;

public interface IGeneralSettingsQueryService
{
    Task<GeneralSettings> Handle(GetGeneralSettingsQuery query, CancellationToken cancellationToken);
}
