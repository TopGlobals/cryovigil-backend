using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.Commands;

namespace CryoVigil.API.Settings.Application.CommandServices;

public interface IGeneralSettingsCommandService
{
    Task<GeneralSettings> Handle(UpdateGeneralSettingsCommand command, CancellationToken cancellationToken);
}
