using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Queries;

namespace CryoVigil.API.Settings.Application.QueryServices;

public interface ISettingsQueryService
{
    Task<IEnumerable<GeneralSettings>> Handle(GetAllSettingsQuery query, CancellationToken cancellationToken);

    Task<GeneralSettings?> Handle(GetSettingsByIdQuery query, CancellationToken cancellationToken);
}
